create table "User"(
	"id" uuid default gen_random_uuid() NOT NULL,
	login varchar(30) unique NOT NULL,
	"password" varchar(30) NOT NULL
		constraint correct_password check(length("password") > 6),
	"name" varchar(50) NOT NULL,
	email varchar(50) NOT NULL,
	pin char(5) 
		constraint correct_pin check(pin ~ '^[0-9]{5}$'),
	PRIMARY KEY("id")
);

insert into "User" values
(default, 'admin', '1234567', 'admin', 'email', null),
(default, 'login', 'password','newUser', 'email', '12345')
select * from "User"