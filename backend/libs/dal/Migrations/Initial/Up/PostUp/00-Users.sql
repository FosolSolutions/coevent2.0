SET IDENTITY_INSERT dbo.[User] ON

INSERT INTO dbo.[User] (
  [Id]
  , [Username]
  , [Email]
  , [Key]
  , [Password]
  , [DisplayName]
  , [FirstName]
  , [MiddleName]
  , [LastName]
  , [IsEnabled]
  , [FailedLogins]
  , [UserType]
  , [EmailVerified]
  , [VerifiedOn]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'admin' -- Username
  , 'admin@fosol.ca' -- Email
  , '24e8ae15-848f-44ee-8a79-e014f89c538e' -- Key
  , '' -- Password
  , 'Administrator' -- DisplayName
  , 'System' -- FirstName
  , '' -- MiddleName
  , 'Administrator' -- LastName
  , 1 -- IsEnabled
  , 0 -- FailedLogins
  , 1 -- UserType.User
  , 1 -- EmailVerified
  , null -- VerfiedOn
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[User] OFF
