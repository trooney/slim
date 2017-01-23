
build:
	cp -a Slim/bin build/bin
	cp -a Slim/App_Data/ build/App_Data
	cp -a Slim/Assets/ build/Assets
	cp -a Slim/Views/ build/Views
	cp -a Slim/Global.asax build/
	cp -a Slim/Web.config build/

.PHONY: build