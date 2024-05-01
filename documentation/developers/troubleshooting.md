# Troubleshooting

## Local access error
When running the solution in Visual Studio, you may reach an access error. There are a number of ways you can try to resolve this:
- Ensure you are running visual studio in administrator mode
- Reduce the directory path length
- Ensure your repository is not cloned in your Onedrive directory.
- Add the parent folder to your Windows Defender Exclusion list.
  - To do this, open a powershell terminal in administrator mode.
  - Run the following command to exclude the parent folder:
```
Add-MpPreference -ExclusionPath 'C:\your\path\here'
```

## Policy issue when running scripts
If you encounter an execution policy issue, you might need to set the execution policy to allow running scripts. Run PowerShell as an administrator and use the following command:
```
Set-ExecutionPolicy RemoteSigned
```