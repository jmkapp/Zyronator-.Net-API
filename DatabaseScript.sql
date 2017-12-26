use ZyronatorDB

drop table if exists dbo.DiscogsUserList
drop table if exists dbo.ApplicationUser

create table dbo.ApplicationUser
(
	UserId int not null identity(1, 1) primary key,
	UserName varchar(MAX) not null,
	UserPassword varchar(MAX) not null
)

create table dbo.DiscogsUserList
(
	ListId int not null identity(1, 1) primary key,
	DiscogsId int unique not null,
	ListName varchar(MAX) not null,
	ResourceUrl varchar(MAX) not null,
	Uri varchar(MAX) not null,
	ListDescription varchar(MAX)
)

insert into dbo.DiscogsUserList
(
	DiscogsId,
	ListName,
	ResourceUrl,
	Uri,
	ListDescription
)
values
(
	373143,
	'(150613) Zyron Live on ISFM',
	'https://api.discogs.com/lists/373143',
	'https://www.discogs.com/lists/150613-Zyron-Live-on-ISFM/373143',
	'Records played in this stream.\nhttp://zyron.c64.org/mixinfo.php?mixid=182&t=dj-zyron-live-on-isfm-2015-06-13'
)