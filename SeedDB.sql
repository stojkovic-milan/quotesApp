insert [Quotes3].[dbo].[Users] ([Id],[Email],[PasswordHash])
select '{27498a6a-9f79-4004-2b23-08dc4a91e40a}',N'milanstojkovic2707@gmail.com',N'$2a$11$2X6.lHoY6jknnijriF.Eie.h/1ydZUb6dxkpwyfwntNCafRpLjC8q' UNION ALL
select '{a6996352-aa4c-4a68-bf38-08dc4c12cd43}',N'test@fazi.rs',N'$2a$11$/nwkC7FjeWPwd/udwt2gVeu7CM3Vp41UikVLFMFLcBlnROkAg6spq';

insert [Quotes3].[dbo].[Quotes] ([Id],[Content],[Author],[Tags],[NegativeCount],[PositiveCount])
select '{8a583b25-f857-4f83-a238-08dc4a623edf}',N'Be the change that you wish to see in the world',N'Mahatma Gandhi',N'["Motivational"]',1,4 UNION ALL
select '{774554fe-777d-4e86-177b-08dc4a9a1817}',N'In three words I can sum up everything I''ve learned about life: it goes on.',N'Robert Frost',N'["Life"]',2,2 UNION ALL
select '{b7af88be-b105-42b2-177c-08dc4a9a1817}',N'You only live once, but if yo do it right, once is enough',N'MAE WEST',N'["Motivational"]',5,0 UNION ALL
select '{e8f7f718-85d9-4255-177d-08dc4a9a1817}',N'A room without books is like a body wihtout a soul',N'Marcus Tullius Cicero',N'["Art"]',4,2 UNION ALL
select '{569e05db-d211-460b-177e-08dc4a9a1817}',N'So many books, so little time.',N'Frank Zappa',N'["Art"]',1,2 UNION ALL
select '{7304b7b2-13ab-4552-177f-08dc4a9a1817}',N'Be yourself; everyone else is already taken.',N'Oscar Wilde',N'["Life","Motivational"]',1,10 UNION ALL
select '{0c4fd0b5-1abe-4d46-1780-08dc4a9a1817}',N'Two things are infinite: the universe and human stupidity; and I''m not sure about the universe.',N'Albert Einstein',N'["Art"]',0,0 UNION ALL
select '{98704145-4c6d-41bf-1781-08dc4a9a1817}',N'If you tell the truth, you don''t have to remember anything.',N'Mark Twain',N'["Life"]',0,6 UNION ALL
select '{469bd8b0-00e3-4ea7-1782-08dc4a9a1817}',N'I''ve learned that people will forget what you said, people will forget what you did, but people will never forget how you made them feel.',N'Maya Angelou',N'[]',2,3 UNION ALL
select '{63686573-d1b5-46b6-1783-08dc4a9a1817}',N'To live is the rarest thing in the world. Most people exist, that is all',N'Oscar Wilde',N'["Life"]',5,6 UNION ALL
select '{962d6956-4ba3-4c6d-1784-08dc4a9a1817}',N'Here''s to the crazy ones. The misfits. The rebels. The troublemakers. The round pegs in the square holes. The ones who see things differently. They''re not fond of rules. And they have no respect for the status quo. You can quote them, disagree with them, glorify or vilify them. About the only thing you can''t do is ignore them. Because they change things. They push the human race forward. And while some may see them as the crazy ones, we see genius. Because the people who are crazy enough to think they can change the world, are the ones who do.',N'Steve Jobs',N'[]',1,0;

insert [Quotes3].[dbo].[Ratings] ([Id],[Positive],[QuoteId],[UserId])
select '{9ab2c591-7472-42e9-584c-08dc4ad47c3f}',0,'{962d6956-4ba3-4c6d-1784-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{b953497b-2177-460b-269b-08dc4b5bbaf7}',1,'{8a583b25-f857-4f83-a238-08dc4a623edf}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{333440c7-02b0-42dc-95ba-08dc4b85885b}',0,'{774554fe-777d-4e86-177b-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{d8edea3c-c61f-4102-95c4-08dc4b85885b}',0,'{b7af88be-b105-42b2-177c-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{c321ae94-0149-4b57-95cf-08dc4b85885b}',0,'{569e05db-d211-460b-177e-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{e481be19-0380-45e9-1a31-08dc4c072ae3}',0,'{7304b7b2-13ab-4552-177f-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{b53bdc0b-e7e6-4caa-9c10-08dc4c095db4}',1,'{e8f7f718-85d9-4255-177d-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}' UNION ALL
select '{06d5c74d-aedd-47f9-9c13-08dc4c095db4}',1,'{98704145-4c6d-41bf-1781-08dc4a9a1817}','{27498a6a-9f79-4004-2b23-08dc4a91e40a}';

