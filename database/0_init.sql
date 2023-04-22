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
    created_at timestamp default now() not null,
    updated_at timestamp,
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
    created_at timestamp default now() not null,
    updated_at timestamp,
    constraint contractors_pk primary key (`id`),
    constraint contractors_addresses_id_fk foreign key (`addresses_id`) references addresses (`id`) ON DELETE SET NULL
);
