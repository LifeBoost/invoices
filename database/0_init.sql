create table if not exists addresses
(
    id varchar(36) not null unique,
    users_id varchar(36) not null,
    name varchar(255) not null,
    street varchar(255) not null,
    zip_code varchar(7) not null,
    city varchar(255) not null,
    created_at datetime default now() not null,
    updated_at datetime,
    constraint addresses_pk primary key (`id`)
);

create table if not exists companies
(
    id varchar(36) not null unique,
    users_id varchar(36) not null,
    addresses_id varchar(36),
    name varchar(255) not null,
    identification_number varchar(20) not null,
    is_vat_payer boolean not null,
    vat_rejection_reason int,
    email varchar(255),
    phone_number varchar(255),
    created_at datetime default now() not null,
    updated_at datetime,
    constraint companies_pk primary key (`id`),
    constraint companies_addresses_id_fk foreign key (`addresses_id`) references addresses (`id`) ON DELETE SET NULL
);

create table if not exists contractors
(
    id varchar(36) not null unique,
    users_id varchar(36) not null,
    addresses_id varchar(36),
    name varchar(255) not null,
    identification_number varchar(255) not null,
    created_at datetime default now() not null,
    updated_at datetime,
    constraint contractors_pk primary key (`id`),
    constraint contractors_addresses_id_fk foreign key (`addresses_id`) references addresses (`id`) ON DELETE SET NULL
);

create table if not exists bank_accounts
(
    id varchar(36) not null unique,
    users_id varchar(36) not null,
    companies_id varchar(36) not null,
    name varchar(255) not null,
    bank_name varchar(255) not null,
    bank_account_number varchar(255) not null,
    currency_code enum('PLN', 'USD', 'EUR') not null,
    created_at datetime default now() not null,
    updated_at datetime,
    constraint bank_accounts_pk primary key (`id`), 
    constraint bank_accounts_companies_id_fk foreign key (`companies_id`) references companies (`id`) ON DELETE CASCADE
);

create table if not exists invoices
(
    id varchar(36) not null unique,
    users_id varchar(36) not null,
    companies_id varchar(36) not null,
    contractors_id varchar(36),
    bank_accounts_id varchar(36),
    status enum('draft', 'send', 'paid', 'deleted') not null,
    number varchar(255),
    total_amount int(11),
    tax int(11),
    currency enum('PLN', 'USD', 'EUR'),
    generated_at datetime,
    sold_at datetime,
    json_parameters longtext not null,
    created_at datetime default now() not null,
    updated_at datetime,
    constraint invoices_pk primary key (`id`),
    constraint invoices_companies_id_fk foreign key (`companies_id`) references companies (`id`) ON DELETE CASCADE,
    constraint invoices_contractors_id_fk foreign key (`contractors_id`) references contractors (`id`) ON DELETE SET NULL,
    constraint invoices_bank_accounts_id_fk foreign key (`bank_accounts_id`) references invoices (`id`) ON DELETE SET NULL
);