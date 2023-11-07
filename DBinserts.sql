-- Active: 1699354628012@@185.252.146.21@5432@MyCashierDB
insert into "User" values
(default, 'admin', '1234567', 'admin', 'email');

insert into "Currency" VALUES
('RUB', 'Российский рубль'),
('BTC', 'Биткоин'),
('USD', 'Доллар США'),
('EUR', 'Евро');

insert into "Account" values
(default, 'Admin Account', default, 'RUB',
(select "id" from "User" where "login" = 'admin'));

insert into "TransactionType" values
(default, 'Доход'),
(default, 'Расход');

insert into "Category" values 
(default, 'Развлечения', null);

insert into "Transaction" values 
(   
    default, 
    (select "id" from "TransactionType" where "name" = 'Расход'), 
    (select "id" from "Account" where "name" = 'Admin Account'),
    'RUB', 300, 
    '10.09.2023',
    (select "id" from "Category" where "name" = 'Развлечения'),
    'Вкусно поел и посмотрел Барби, Гигача'
);

insert into "ObligationType" values 
(default, 'Одолжение'),
(default, 'Долг');

insert into "ObligationStatus" values 
(default, 'Выплачено'),
(default, 'Прощено');

insert into "Obligation" values 
(
    default, 
    (select "id" from "ObligationType" where "name" = 'Долг'), 
    true, 
    (select "id" from "ObligationStatus" where "name" = 'Прощено'),
    (select "id" from "Account" where "name" = 'Admin Account'),
    'Вася',
    'RUB',
    1000,
    '10.22.2023',
    (select "id" from "Category" where "name" = 'Развлечения'),
    'Васе не хватило на билет в театр'
);