# Release Procedure
Welcome to the very hectic, very tedious and very manual release process.
- Tools to use for markdown: [Visual Studio Code](https://code.visualstudio.com/).
- *Performing release changes in a branch enables higher quality of release.*.

## a. Branching and code changes
1. Create the new release version branch locally, ``vX.X.X``
    - Use [semantic versioning](http://semver.org/) whenever possible.
    - Add the 4th ``.X`` as adhoc patch to previous released version.
1. Update `common\VersionInfo.cs`
    ```csharp
    [assembly: AssemblyFileVersion("X.X.X.0")]
    [assembly: AssemblyInformationalVersion("X.X.X.0")]
    ```
1. Commit and push branch to GitHub.

## b. Issues, milestones
1. Find all closed issues against the releasing [milestone](https://github.com/zionyx/jenkins-tray/milestones).
1. Assure all closed issues have the correct milestone set.

## c. Drafting the new release note
1. [Draft a new release](https://github.com/zionyx/jenkins-tray/releases/new).
1. Tag the pushed branch with `-alpha` postfix. Eg:
    - Enter `vX.X.X-alpha` in Tag version.
    - Pick the releasing branch from the drop down.
1. Release title: ``Jenkins Tray - vX.X.X``
1. Description sample:
    ```
    Minor update release - Version 1.0.5

    Enhancements / bugfixes include:
    * Jenkins Error 500 when trying to claim a build with Claim Plugin v2.5 and above. #70 
    * Ability to stop / cancel ongoing builds. #66 
    ```
1. Check **This is a pre-release**.
1. Publish pre-release.

## d. Building the release
1. Check [AppVeyor](https://ci.appveyor.com/project/zionyx/jenkins-tray-tracker) for build status.
1. Get build output from build server.
    - *It builds internally due to DevExpress license constraint.*
1. Ensure the MSI filename has correct version postfix.
    - ``JenkinsTray_vX.X.X.0.msi``
1. Authenticode sign the MSI file and ensure the signature is correct.
1. Test install and upgrades.
    - :warning: *Don't screw up, v1.0.4 introduced installation upgrade problems!*
1. Upload the MSI file to the [pre-release](https://github.com/zionyx/jenkins-tray/releases).
1. Update pre-release.

## e. More commits (Overhead)
1. [Go to the pre-release](https://github.com/zionyx/jenkins-tray/releases/).
1. Copy the link to the uploaded MSI file from Downloads section in the pre-release.
1. Update `scripts\version.properties`:
    - Paste the link to `version.installerUrl`.
    - Remove the `-alpha` from the link.
    - :warning: *Simply because the release is not an alpha.* (What a crappy process.)
    ```
    version.number=X.X.X.0`
    version.installerUrl=https://github.com/zionyx/jenkins-tray/releases/download/vX.X.X/JenkinsTray_vX.X.X.0.msi
    ```
1. Commit and push changes to branch to GitHub.

## f. Real release, more commits
1. Edit the pre-release notes.
1. Create a new release tag without `-alpha` postfix based on the same branch. Eg: `vX.X.X`.
   - :warning: Tag name has to match the `version.installerUrl` in `scripts\version.properties` file.
1. Uncheck **This is a pre-release**.
1. Publish the release.
1. Test download link.
1. Merge branch to master, and push.
    ```
    git checkout master
    git merge vX.X.X
    git push origin
    ```
1. Test with previous JenkinsTray version for update notification and upgrade procedure.

## g. Last few commits (!@#$%^&*)
1. Delete the branch, and push.
    ```
    git branch --delete vX.X.X
    git push origin --delete heads/vX.X.X
    ```
1. Delete the ``-alpha`` tag, and push.
    ```
    git tag --delete vX.X.X
    git push origin --delete tags/vX.X.X
    ```
    You can also [delete the tag from UI](https://github.com/zionyx/jenkins-tray/tags).
