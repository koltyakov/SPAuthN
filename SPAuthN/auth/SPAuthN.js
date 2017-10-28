(() => {

    const AuthConfig = require('node-sp-auth-config').AuthConfig;
    const spAuth = require('node-sp-auth');

    const utils = require('../auth/utils');

    return (args, callback) => {

        let authOptions = Object.assign({}, {
            configPath: './config/private.json',
            encryptPassword: true,
            saveConfigOnDisk: true,
        }, utils.stringToArgvs(args));

        const authConfig = new AuthConfig(authOptions);

        authConfig.getContext()
            .then(context => {
                return spAuth.getAuth(context.siteUrl, context.authOptions);
            })
            .then(options => {
                // callback(null, options.headers);
                callback(null, Object.keys(options.headers).map((prop) => {
                    return `${prop}::${options.headers[prop]}`;
                }).join(';;'));
            })
            .catch(error => {
                callback(error, null);
            });

    };

})();