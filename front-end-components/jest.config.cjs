module.exports = {
    transform: {
      '^.+\\.(ts|tsx)$': 'babel-jest',
    },
    testMatch: ['**/?(*.)+(spec|test).(ts|tsx|js)'],
    testEnvironment: 'jsdom',
    moduleNameMapper: {
        '\\.(css|less|scss)$': '<rootDir>/__mocks__/styleMock.js',
        "src/(.*)": "<rootDir>/src/$1",
    },
  };
  