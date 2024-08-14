namespace DOTNETDemo.Constants
{
    public static class ApiRoutes
    {
        public static class User
        {
            public const string Base = "v1/api/user";

            public const string GetUser = Base + "/GetUser";
            public const string CreateUserCard = Base + "/CreateUserCard";
            public const string DeleteUserCard = Base + "/DeleteUserCard";
            public const string UpdateUserCard = Base + "/UpdateUserCard";
        }
    }
}
