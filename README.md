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

Setup mono, nginx, and fastcgi-mono-server4. You can start the daemon with the following command, but must build your own startup script.

```
fastcgi-mono-server4 /applications=slimurl.example.com:/:/var/www/slimurl.example.com/ /socket=tcp:127.0.0.1:9000 /printlog=True /loglevel=All /verbose=True
```

Deploy
---

Build your project in Xamarin Studio. Then run the `make build` task and rsync the results to your server.

```
make build
rsync --exclude=build/App_Data/slim.sqlite -a build/ username@example:/path/to/slimurl.example.com
```