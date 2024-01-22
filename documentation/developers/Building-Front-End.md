# Building the front end MVC - EducationBenchmarking\.Web

To use the GOV UK Design System locally please follow the step below:

- first cd to the following directory, assuming you are at the root of the repo

```cd .\web\src\EducationBenchmarking.Web```

- to install the required packages via npm use

```npm i```

- run the gulp script with functions to build the sass and copy over the assets required from the packages installed

```npm run build```

Now when you run the project locally you should see the correct styling and assets.


## Notes

using node 20.10.0

using npm 10.2.3

## Troubleshooting

### Unable to authenticate, your authentication token seems to be invalid

`.npmrc` defines a custom registry with which one must first be authenticated before packages may be installed.