const webpack = require('webpack');
const path = require('path');
const UglifyJSPlugin = require('uglifyjs-webpack-plugin');
const StringReplacePlugin = require('string-replace-webpack-plugin');

module.exports = {
  mode: 'production',
  cache: false,
  entry: './SPAuthN.js',
  target: 'node',
  output: {
    path: path.join(__dirname, '..', 'SPAuthN/Resources'),
    filename: 'SPAuthN.js',
    library: 'SPAuthN',
    libraryTarget: 'var'
  },
  module: {
    rules: [
      {
        test: /rx\.lite\.aggregates\.js/,
        use: 'imports-loader?define=>false'
      },
      {
        test: /OnDemand\.js/,
        loader: StringReplacePlugin.replace({
          replacements: [
            {
              pattern: `path.join(__dirname, 'electron/main.js')`,
              replacement: () => `path.join(process.cwd(), './electron/main.js')`
            }
          ]}
        )
      }
    ]
  },
  plugins: [
    new UglifyJSPlugin({ sourceMap: true }),
    new StringReplacePlugin(),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify('production')
    })
  ]
};
