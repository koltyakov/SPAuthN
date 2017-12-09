const webpack = require('webpack');
const path = require('path');
const UglifyJSPlugin = require('uglifyjs-webpack-plugin');

module.exports = {
  entry: './SPAuthN.js',
  target: 'node',
  output: {
    path: path.join(__dirname, '..', 'SPAuthN/Resources'),
    filename: 'SPAuthN.js',
    library: 'SPAuthN',
    libraryTarget: 'var'
  },
  module: {
    rules: [{
      test: /rx\.lite\.aggregates\.js/,
      use: 'imports-loader?define=>false'
    }]
  },
  plugins: [
    new UglifyJSPlugin(),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify('production')
    })
  ]
};
