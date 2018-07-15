"use strict"
{
    let path = require('path');
    const CleanWebpackPlugin = require('clean-webpack-plugin');
    const bundleFolder = "wwwroot/bundle/";

    module.exports = {
        entry: "./node_modules/markdown/lib/markdown.js",
        output: {
            filename: 'markdown.js',
            path: path.resolve(__dirname, bundleFolder)
        },
        plugins: [
            new CleanWebpackPlugin([bundleFolder])
        ]
    };
}