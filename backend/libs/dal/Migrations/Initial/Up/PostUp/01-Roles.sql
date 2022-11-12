SET IDENTITY_INSERT dbo.[Role] ON

INSERT INTO dbo.[Role] (
  [Id]
  , [Name]
  , [Key]
  , [AccountId]
  , [SortOrder]
  , [IsEnabled]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'Administrator' -- Name
  , 'e67ce6f3-18e5-483a-9a70-dd34b23a4c92' -- Key
  , 1 -- AccountId
  , 0 -- SortOrder
  , 1 -- IsEnabled
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[Role] OFF
