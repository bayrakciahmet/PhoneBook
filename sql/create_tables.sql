

Create Table Report(
Id serial primary key,
reportname varchar(200),
Status varchar(30) null,
RequestDate timestamp not null default CURRENT_TIMESTAMP
);

CREATE TABLE reportlocation (
    Id serial PRIMARY KEY,
    ReportId int REFERENCES report(id) ON DELETE CASCADE,
    LocationName varchar(100) NOT NULL,
    PersonCount int,
    PhoneNumberCount int,
    ReportDate timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);