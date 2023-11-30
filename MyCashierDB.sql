-- Active: 1699354628012@@185.252.146.21@5432@MyCashierDB

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
	"name" varchar(50) unique NOT NULL, --Имя
	"balance" decimal default 0 NOT NULL, --Текущий баланс
	"currencyID" char(3) references "Currency"("id") NOT NULL, --Валюта
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
    "currencyID" char(3) references "Currency"("id") NOT NULL,
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
    "statusID" uuid references "ObligationStatus"("id") NOT NULL,
    "accountID" uuid references "Account"("id") NOT NULL,
    "debtor" varchar(50) NOT NULL,
    "currencyID" char(3) references "Currency"("id") NOT NULL,
    "sum" decimal NOT NULL,
    "date" date NOT NULL,
    "description" text,
    PRIMARY KEY("id")
);


create view "TransactionsJournal"
as select
    "Transaction"."id" as "TransactionID",
    "User"."name" as "User",
    "TransactionType"."name" as "TransactionTypeName",
    "Account"."name" as "AccountName",
    "Transaction"."currencyID" as "Currency",
    "Transaction"."sum" as "Sum",
    "Transaction"."date" as "Date",
    "Category"."name" as "Category",
    "Transaction"."description" as "Description"
    from 
    "TransactionType", "Account", "Category", "Transaction", "User"
    where
    "TransactionType"."id" = "Transaction"."typeID" and
    "Account"."id" = "Transaction"."accountID" and
    "Account"."userID" = "User"."id" and
    "Category"."id" = "Transaction"."categoryID";
create view "ObligationsJournal"
as select
    "Obligation"."id" as "ObligationID",
    "User"."name" as "User",
    "ObligationType"."name" as "ObligationTypeName",
    "ObligationStatus"."name" as "StatusName",
    "Account"."name" as "AccountName",
    "Obligation"."debtor" as "Debtor",
    "Obligation"."currencyID" as "Currency",
    "Obligation"."sum" as "Sum",
    "Obligation"."date" as "Date",
    "Obligation"."description" as "Description"
    from 
    "ObligationType", "ObligationStatus", "Account", "Obligation", "User"
    where 
    "ObligationType"."id" = "Obligation"."typeID" and
    "ObligationStatus"."id" = "Obligation"."statusID" and
    "Account"."id" = "Obligation"."accountID" and
    "Account"."userID" = "User"."id";

    select * from "TransactionsJournal";
    select * from "ObligationsJournal";
