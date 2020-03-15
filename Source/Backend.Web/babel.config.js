module.exports = function(api) {
  api.cache(true);

  const presets = [
    [
      "@babel/preset-env",
      {
        corejs: 3,
        modules: false,
        useBuiltIns: "entry"
      }
    ]
  ];

  const plugins = [
    "@babel/plugin-syntax-dynamic-import",
    "@babel/plugin-transform-async-to-generator",
    [
      "@babel/plugin-transform-runtime",
      {
        corejs: 3
      }
    ]
  ];

  return {
    presets,
    plugins
  };
};
