--select * from Videos

--select * from VideoCategory

--select * from Categories

declare @VideoId as int
declare @temp table
(
	CatId int
)

set @VideoId = 4

insert into @temp
select VideoCatId from VideoCategory where VideoId = @VideoId

select * from @temp

select * from Videos as v
inner join VideoCategory as vc
	on vc.VideoId = v.VideoId
where 
	vc.VideoCatId in (select CatId from @temp)