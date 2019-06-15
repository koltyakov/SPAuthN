const webpack = require('webpack');
const path = require('path');
const TerserPlugin = require('terser-webpack-plugin');
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
              pattern: `'--', this._siteUrl, this._authOptions.force.toString()]`,
              replacement: () => `this._siteUrl, this._authOptions.force.toString()]`
            },
            {
              pattern: `path.join(__dirname, 'electron/main.js')`,
              replacement: () => `path.join(process.cwd(), 'electron/main.js')`
            },
            {
              pattern: `choices.push(new inquirer_1.Separator());`,
              replacement: () => `choices.push(new inquirer_1.Separator('---'));`
            }
          ]}
        )
      }
    ]
  },
  optimization: {
    minimizer: [
      new TerserPlugin({
        cache: true,
        parallel: true,
        sourceMap: false,
        extractComments: 'all'
      })
    ]
  },
  plugins: [
    new StringReplacePlugin(),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify('production')
    })
  ]
};
