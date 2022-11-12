SET IDENTITY_INSERT dbo.[Account] ON

INSERT INTO dbo.[Account] (
  [Id]
  , [Key]
  , [Name]
  , [AccountType]
  , [IsEnabled]
  , [OwnerId]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , '94fa9af3-6889-49ab-bff5-dda84d35e84d' -- Key
  , 'CoEvent' -- Name
  , 0 -- AccountType.Free
  , 1 -- IsEnabled
  , 1 -- OwnerId
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[Account] OFF
