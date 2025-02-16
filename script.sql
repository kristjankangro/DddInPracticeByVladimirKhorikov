use master
go

grant connect on database :: master to []
go

grant connect, execute on database :: master to []
go

grant connect on database :: master to []
go

grant connect on database :: master to []
go

grant view any column encryption key definition, view any column master key definition on database :: master to []
go

use DddInPractice
go

grant connect on database :: DddInPractice to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: DddInPractice to [public]
go

create table Ids
(
    EntityName varchar(50) not null,
    NextHight  int         not null
)
    go

create table Snack
(
    SnackId bigint identity
        constraint Snack_pk
            primary key,
    Name    varchar(50) not null
)
    go

create table SnackMachine
(
    SnackMachineId bigint identity
        constraint SnackMachine_pk
            primary key,
    Cents10Count   int not null,
    Cents25Count   int not null,
    Cents1Count    int not null,
    Dollar1Count   int not null,
    Dollar5Count   int not null,
    Dollar20Count  int not null
)
    go

create table Slot
(
    SlotId         bigint identity,
    Quantity       int    not null,
    Price          decimal,
    SnackMachineId bigint not null
        constraint Slot_SnackMachine_SnackMachineId_fk
            references SnackMachine,
    SnackId        bigint not null
        constraint Slot_Snack_SnackId_fk
            references Snack,
    Position       int    not null
)
    go

use master
go

grant connect sql to ##MS_AgentSigningCertificate##
go

use DddInPractice
go

use master
go

grant connect sql to ##MS_PolicyEventProcessingLogin##
go

use DddInPractice
go

use master
go

grant control server, view any definition to ##MS_PolicySigningCertificate##
go

use DddInPractice
go

use master
go

grant connect sql, view any definition, view server state to ##MS_PolicyTsqlExecutionLogin##
go

use DddInPractice
go

use master
go

grant authenticate server to ##MS_SQLAuthenticatorCertificate##
go

use DddInPractice
go

use master
go

grant authenticate server, view any definition, view server state to ##MS_SQLReplicationSigningCertificate##
go

use DddInPractice
go

use master
go

grant view any definition to ##MS_SQLResourceSigningCertificate##
go

use DddInPractice
go

use master
go

grant view any definition to ##MS_SmoExtendedSigningCertificate##
go

use DddInPractice
go

use master
go

grant view any database to [public]
go

use DddInPractice
go

use master
go

grant connect sql to sa
go

use DddInPractice
go

