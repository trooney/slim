Slim
----

Slim is a simple url minification application. It's written in C# and uses the Mono framework.

Slim uses the following components:

- ASP MVC4
- TinyIoC
- sqlite-net
- AutoMapper

Running
---

Setup mono, nginx, and fastcgi-mono-server4. You can start the daemon with the following command:

```
fastcgi-mono-server4 /applications=slimurl.example.com:/:/var/www/slimurl.example.com/ /socket=tcp:127.0.0.1:9000 /printlog=True /loglevel=All /verbose=True
```

Installing
---

Start-up script example is provide in /extras

```
chmod +x /etc/init.d/monoserve
update-rc.d monoserve defaults
```

Note: Ensure that both the /App_Data directory and the /App_Data/slim.sqlite file exist and are read-writable by the webserver user.

Deploy
---

Build your project in Xamarin Studio. Then run the `make build` task and rsync the results to your server.

```
make build
rsync -avz --exclude 'App_Data*'  --exclude '*.sqlite' build/ username@example:/path/to/slimurl.example.com
```