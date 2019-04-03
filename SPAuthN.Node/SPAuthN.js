//@ts-check

const { AuthConfig } = require('node-sp-auth-config');
const spAuth = require('node-sp-auth');

const utils = require('./utils');

const auth = (args, callback) => {

  const authOptions = Object.assign({}, {
    configPath: './config/private.json',
    encryptPassword: true,
    saveConfigOnDisk: true,
    headlessMode: false
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
      const response = Object.assign({}, result[0], result[1]);
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
