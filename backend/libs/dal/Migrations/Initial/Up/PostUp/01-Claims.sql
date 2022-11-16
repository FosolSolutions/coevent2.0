SET IDENTITY_INSERT dbo.[Claim] ON

INSERT INTO dbo.[Claim] (
  [Id]
  , [Name]
  , [Description]
  , [AccountId]
  , [IsEnabled]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'administrator' -- Name
  , '' -- Description
  , 1 -- AccountId
  , 1 -- IsEnabled
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
), (
  2 -- Id
  , 'participant' -- Name
  , '' -- Description
  , 1 -- AccountId
  , 1 -- IsEnabled
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[Claim] OFF
