const path = require("path");
const webpack = require("webpack");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCssPlugin = require("optimize-css-assets-webpack-plugin");
const MomentLocalesPlugin = require("moment-locales-webpack-plugin");
const VueLoaderPlugin = require("vue-loader/lib/plugin");
const bundleOutputDir = "./wwwroot/dist";

module.exports = () => {
  const isDevBuild = !(process.env.NODE_ENV && process.env.NODE_ENV === "production");
  return [
    {
      mode: isDevBuild ? "development" : "production",
      devtool: isDevBuild ? "inline-source-map" : false,
      devServer: {
        publicPath: path.join(__dirname, bundleOutputDir),
        contentBase: path.resolve(__dirname, "./wwwroot"),
        liveReload: false,
        hot: true,
        inline: true,
        writeToDisk: true,
        compress: true,
        // quiet: true,
        https: true,
        port: 5003,
        pfx: "../../Tools/localhost.pfx",
        pfxPassphrase: "localhost"
      },
      stats: { modules: false },
      entry: { site: isDevBuild ? ["webpack-dev-server/client?https://localhost:5003", "webpack/hot/dev-server", "./ClientApp/boot-app.js"] : "./ClientApp/boot-app.js" },
      resolve: {
        extensions: [".js", ".vue"],
        alias: {
          vue$: "vue/dist/vue",
          root: path.resolve(__dirname, "./ClientApp"),
          assets: path.resolve(__dirname, "./ClientApp/assets"),
          components: path.resolve(__dirname, "./ClientApp/components"),
          config: path.resolve(__dirname, "./ClientApp/config"),
          utils: path.resolve(__dirname, "./ClientApp/utils"),
          views: path.resolve(__dirname, "./ClientApp/views")
        }
      },
      module: {
        rules: [
          { test: /\.vue$/, include: /ClientApp/, use: "vue-loader" },
          { test: /\.js$/, include: /ClientApp/, use: "babel-loader" },
          { test: /\.css$/, use: isDevBuild ? ["style-loader", "css-loader"] : [MiniCssExtractPlugin.loader, "css-loader"] },
          {
            test: /\.(png|jp(e)?g|gif|svg)(\?v=\d+\.\d+\.\d+)?$/i,
            use: [
              {
                loader: "url-loader",
                options: {
                  esModule: false,
                  limit: 8192,
                  outputPath: "img",
                  name: isDevBuild ? "[name].[ext]" : "[name].[contenthash].[ext]"
                }
              }
            ]
          },
          {
            test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/i,
            use: [
              {
                loader: "file-loader",
                options: {
                  outputPath: "fonts",
                  name: isDevBuild ? "[name].[ext]" : "[name].[contenthash].[ext]"
                }
              }
            ]
          }
        ]
      },
      output: {
        path: path.join(__dirname, bundleOutputDir),
        publicPath: "/dist/",
        filename: isDevBuild ? "[name].js" : "[name].[contenthash].js",
        chunkFilename: isDevBuild ? "[name].js" : "[name].[chunkhash].js",
        hotUpdateMainFilename: "../hot/hot-update.json", // [hash].hot-update.json
        hotUpdateChunkFilename: "../hot/hot-update.js" // [id].[hash].hot-update.js
      },
      optimization: {
        chunkIds: "named",
        moduleIds: "named",
        runtimeChunk: "single",
        splitChunks: {
          cacheGroups: {
            vendor: {
              test: /[\\/]node_modules[\\/]/,
              name: "vendor",
              chunks: "initial",
              enforce: true
            }
          }
        }
      },
      plugins: [
        // new CleanWebpackPlugin({
        //   cleanStaleWebpackAssets: false
        // }),
        new CopyWebpackPlugin(
          [
            {
              from: "./ClientApp/assets/pwa",
              to: "../pwa"
            }
          ],
          { copyUnmodified: true }
        ),
        new webpack.ProvidePlugin({
          $: "jquery",
          jQuery: "jquery",
          Popper: ["popper.js", "default"]
        }),
        new MiniCssExtractPlugin({
          filename: isDevBuild ? "[name].css" : "[name].[contenthash].css",
          chunkFilename: isDevBuild ? "[name].css" : "[name].[chunkhash].css"
        }),
        new MomentLocalesPlugin({
          localesToKeep: ["pt-br"]
        }),
        new VueLoaderPlugin()
      ].concat(
        isDevBuild
          ? [new webpack.HotModuleReplacementPlugin()]
          : [
              new webpack.HashedModuleIsPlugin(),
              new OptimizeCssPlugin({
                cssProcessorOptions: {
                  safe: true
                }
              })
            ]
      )
    }
  ];
};
