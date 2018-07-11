"use strict"
{
    let path = require('path');
    const CleanWebpackPlugin = require('clean-webpack-plugin');
    const bundleFolder = "wwwroot/bundle/";

    module.exports = {
        entry: "./Scripts/main.js",
        output: {
            filename: 'script.js',
            path: path.resolve(__dirname, bundleFolder)
        },
        plugins: [
            new CleanWebpackPlugin([bundleFolder])
        ]
    };
}