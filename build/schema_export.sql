
    drop table if exists User

    drop table if exists Post

    create table User (
        Id  integer,
       Email TEXT,
       primary key (Id)
    )

    create table Post (
        Id  integer,
       Title TEXT,
       Body TEXT,
       CreatedDate DATETIME,
       ModifiedDate DATETIME,
       AuthorId INTEGER,
       primary key (Id)
    )
