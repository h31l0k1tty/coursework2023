-- Active: 1699354628012@@185.252.146.21@5432@MyCashierDB
insert into "User" values
(default, 'admin', '1234567', 'admin', 'email');

insert into "Currency" values
('RUB', 'Российский рубль'),
('BTC', 'Биткоин'),
('USD', 'Доллар США'),
('EUR', 'Евро'),
('CNY', 'Китайский юань');

insert into "Account" values
(default, 'Admin Account', default, 'RUB',
(select "id" from "User" where "login" = 'admin'));

insert into "TransactionType" values
(default, 'Доход'),
(default, 'Расход');

insert into "Category" values
(default, 'Развлечения', null),
(default, 'Еда', null),
(default, 'Транспорт', null),
(default, 'Путешествия', null),
(default, 'Супермаркеты', null),
(default, 'Одежда', null),
(default, 'Интернет покупки', null);

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
(default, 'Должен'),
(default, 'Одолжил');

insert into "ObligationStatus" values 
(default, 'Активно'),
(default, 'Закрыто');

insert into "Obligation" values 
(
    default, 
    (select "id" from "ObligationType" where "name" = 'Одолжил'),
    (select "id" from "ObligationStatus" where "name" = 'Прощено'),
    (select "id" from "Account" where "name" = 'Admin Account'),
    'Васе',
    'RUB',
    1000,
    '10.22.2023',
    'Васе не хватило на билет в театр'
);