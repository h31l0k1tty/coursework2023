-- Active: 1697616242142@@127.0.0.1@5432@MyCashierDB

drop table
    "User",
    "Currency",
    "Account", 
    "TransactionType",
    "Category",
    "Transaction",
    "ObligationType",
    "ObligationStatus",
    "Obligation";
drop view 
    "TransactionsJournal", "ObligationsJournal";

create table "User"(
	"id" uuid default gen_random_uuid() NOT NULL,
	"login" varchar(30) unique NOT NULL,
	"password" varchar(30) NOT NULL
		constraint correct_password check(length("password") > 6),
	"name" varchar(50) NOT NULL,
	"email" varchar(50) NOT NULL,
	--"pin" char(5) 
		--constraint correct_pin check("pin" ~ '^[0-9]{5}$'),
	PRIMARY KEY("id")
);
create table "Currency"(
  "id" char(3) NOT NULL 
    constraint correct_currency_id check("id" ~ '^[A-Z]{3}$'),
  "name" varchar(50) NOT NULL,
  PRIMARY KEY("id")
);
create table "Account"(
	"id" uuid default gen_random_uuid() NOT NULL,
	"name" varchar(50) unique NOT NULL,
	"balance" decimal default 0 NOT NULL,
	"currencyID" char(3) references "Currency"("id") NOT NULL,
	"userID" uuid references "User"("id") NOT NULL,
	PRIMARY KEY("id")
);
create table "TransactionType"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "name" varchar(50) unique NOT NULL,
    PRIMARY KEY("id")
);
create table "Category"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "name" varchar(50) NOT NULL,
    "icon" bytea,
    PRIMARY KEY("id")
);
create table "Transaction"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "typeID" uuid references "TransactionType"("id") NOT NULL,
    "accountID" uuid references "Account"("id") NOT NULL,
    "currency" char(3) references "Currency"("id"),
    "sum" decimal NOT NULL,
    "date" date NOT NULL,
    "categoryID" uuid references "Category"("id") NOT NULL,
    "description" text,
    PRIMARY KEY("id")
);
create table "ObligationType"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "name" varchar(50) unique NOT NULL,
    PRIMARY KEY("id")
);
create table "ObligationStatus"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "name" varchar(50) unique NOT NULL,
    PRIMARY KEY("id")
);
create table "Obligation"(
    "id" uuid default gen_random_uuid() NOT NULL,
    "typeID" uuid references "ObligationType"("id") NOT NULL,
    "isActive" bool NOT NULL,
    "statusID" uuid references "ObligationStatus"("id") NOT NULL,
    "accountID" uuid references "Account"("id") NOT NULL,
    "debtor" varchar(50) unique NOT NULL,
    "currency" char(3) references "Currency"("id"),
    "sum" decimal NOT NULL,
    "date" date NOT NULL,
    "categoryID" uuid references "Category"("id") NOT NULL,
    "description" text,
    PRIMARY KEY("id")
);


create view "TransactionsJournal"
as select 
    "TransactionType"."name" as "Операция",
    "Account"."name" as "Счёт",
    "Transaction"."currency" as "Валюта",
    "Transaction"."sum" as "Сумма",
    "Transaction"."date" as "Дата",
    "Category"."name" as "Категория",
    "Transaction"."description" as "Описание"
    from 
    "TransactionType", "Account", "Category", "Transaction" 
    where 
    "TransactionType"."id" = "Transaction"."typeID" and
    "Account"."id" = "Transaction"."accountID" and
    "Category"."id" = "Transaction"."categoryID";
create view "ObligationsJournal"
as select 
    "ObligationType"."name" as "Операция",
    "Obligation"."isActive" as "Активно",
    "ObligationStatus"."name" as "Статус",
    "Account"."name" as "Счёт",
    "Obligation"."debtor" as "Должник",
    "Obligation"."currency" as "Валюта",
    "Obligation"."sum" as "Сумма",
    "Obligation"."date" as "Дата",
    "Category"."name" as "Категория",
    "Obligation"."description" as "Описание"
    from 
    "ObligationType", "ObligationStatus", "Account", "Category", "Obligation" 
    where 
    "ObligationType"."id" = "Obligation"."typeID" and
    "Account"."id" = "Obligation"."accountID" and
    "Category"."id" = "Obligation"."categoryID";

    select * from "TransactionsJournal";
    select * from "ObligationsJournal";