const AuthConfig = require('node-sp-auth-config').AuthConfig;
const spAuth = require('node-sp-auth');

const utils = require('./utils');

const auth = (args, callback) => {

  let authOptions = Object.assign({}, {
    configPath: './config/private.json',
    encryptPassword: true,
    saveConfigOnDisk: true
  }, utils.stringToArgvs(args));

  const authConfig = new AuthConfig(authOptions);

  authConfig.getContext()
    .then(context => {
      return Promise.all([
        context,
        spAuth.getAuth(context.siteUrl, context.authOptions),
      ]);
    })
    .then(result => {
      delete result[1].options;
      let response = Object.assign({}, result[0], result[1]);
      if (typeof response.settings === 'undefined') {
        response.settings = null;
      }
      if (typeof response.custom === 'undefined') {
        response.custom = null;
      }
      callback(null, response);
    })
    .catch(error => {
      callback(error, null);
    });

};

exports.auth = auth;
