CREATE NONCLUSTERED  INDEX [IX_TblUserCommunication_EmailD_MobileNo]
	ON [Users].[tblUserCommunication]
	(MobileNo,EmailId)
