SET IDENTITY_INSERT dbo.[Claim] ON

INSERT INTO dbo.[Claim] (
  [Id]
  , [ClaimType]
  , [Value]
  , [AccountId]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'role' -- ClaimType
  , 'administrator' -- Value
  , 1 -- AccountId
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[Claim] OFF
