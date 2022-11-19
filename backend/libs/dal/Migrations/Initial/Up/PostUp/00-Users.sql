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
  , [Status]
  , [UserType]
  , [EmailVerified]
  , [EmailVerifiedOn]
  , [CreatedBy]
  , [UpdatedBy]
) VALUES (
  1 -- Id
  , 'admin' -- Username
  , 'admin@fosol.ca' -- Email
  , NEWID() -- Key
  , '' -- Password
  , 'Administrator' -- DisplayName
  , 'System' -- FirstName
  , '' -- MiddleName
  , 'Administrator' -- LastName
  , 1 -- IsEnabled
  , 0 -- FailedLogins
  , 2 -- Status
  , 1 -- UserType.User
  , 0 -- EmailVerified
  , null -- EmailVerifiedOn
  , 'seed' -- CreatedBy
  , 'seed' -- UpdatedBy
)

SET IDENTITY_INSERT dbo.[User] OFF
