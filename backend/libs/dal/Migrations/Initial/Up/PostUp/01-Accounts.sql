SET IDENTITY_INSERT dbo.[Account] ON

INSERT INTO dbo.[Account] (
  [Id]
  , [Name]
  , [AccountType]
  , [IsEnabled]
  , [OwnerId]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'CoEvent' -- Name
  , 0 -- AccountType.Free
  , 1 -- IsEnabled
  , 1 -- OwnerId
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[Account] OFF
