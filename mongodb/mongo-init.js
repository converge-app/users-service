db.auth("admin", "password");

db.createUser({
  user: "application",
  pwd: "password",
  roles: [
    {
      role: "readWrite",
      db: "ApplicationDb"
    }
  ]
});
