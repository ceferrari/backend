module.exports = {
  root: true,
  env: {
    browser: true,
    es6: true,
    node: true
  },
  parser: "vue-eslint-parser",
  parserOptions: {
    parser: "babel-eslint",
    ecmaVersion: 8,
    sourceType: "module",
    ecmaFeatures: {
      modules: true
    }
  },
  plugins: ["vue", "prettier"],
  extends: ["plugin:vue/essential", "plugin:prettier/recommended"],
  rules: {
    "no-console": process.env.NODE_ENV === "production" ? "error" : "off",
    "no-debugger": process.env.NODE_ENV === "production" ? "error" : "off",
    "prettier/prettier": "warn"
  }
};
