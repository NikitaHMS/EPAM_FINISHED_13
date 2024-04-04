namespace Model
{
    public class User
    {
        private string login;
        private string password;

        public User(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public string getLogin() { return login; }

        public void setLogin(string login) { this.login = login; }

        public string getPassword() { return password; }

        public void setPassword(string password) { this.password = password; }
    }
}
