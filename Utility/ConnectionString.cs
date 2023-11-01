namespace StudentManagementSystem.Utility
{
    public static class ConnectionString
    {
        private static string cName = "Server=DESKTOP-DPEU270\\SQLEXPRESS;Database=students;Trusted_Connection=True;TrustServerCertificate=True";
        public static string CName
        {
            get => cName;
        }
    }
}
