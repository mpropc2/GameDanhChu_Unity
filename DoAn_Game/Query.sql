CREATE DATABASE TYPINGGAME
GO

CREATE TABLE CHUDE
(
MaChuDe int PRIMARY KEY,
TenChuDe varchar(50),
NoiDung varchar(500),
MoTa varchar(100),
ThoiGian int,
DiemToiDa int,
CapDoKho int,
TinhTrang bit
);

INSERT INTO CHUDE(MaChuDe, TenChuDe, NoiDung, MoTa, ThoiGian, DiemToiDa, CapDoKho, TinhTrang) VALUES (1,
																									'Tree',
																									'trees branches roots roots stems leaves trees wind water air temperature humidity light shade',
																									'plant parts',
																									2, 100, 1, 1);
INSERT INTO CHUDE(MaChuDe, TenChuDe, NoiDung, MoTa, ThoiGian, DiemToiDa, CapDoKho, TinhTrang) VALUES (2,
																									'Numbers',
																									'one two three four five six seven eight nine ten eleven twelve thirteen fourteen fifteen sixteen seventeen eighteen nineteen twenty',
																									'part of number',
																									2, 100, 2, 1);
select * from CHUDE where TinhTrang=1 and CapDoKho=1