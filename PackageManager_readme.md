# UPM Package Starter Kit

The purpose of this package template starter kit is to provide the data structure and development guidelines for new packages meant for the **Unity Package Manager (UPM)**.

This is the first of many steps towards an automated package publishing experience within Unity. This package template starter kit is merely a fraction of the creation, edition, validation, and publishing tools that we will end up with.

We hope you enjoy your experience. You can use **#devs-packman** on Slack to provide feedback or ask questions regarding your package development efforts.

## Are you ready to become a package?
The Package Manager is a work-in-progress for Unity and, in that sense, there are a few criteria that must be met for your package to be considered on the package list at this time:
- **Your code accesses public Unity C# APIs only.**  If you have a native code component, it will need to ship with an official editor release.  Internal API access might eventually be possible for Unity made packages, but not at this time.
- **Your code doesn't require security, obfuscation, or conditional access control.**  Anyone should be able to download your package and access the source code.


## Package structure

```none
<root>
  ├── package.json
  ├── README.md
  ├── CHANGELOG.md
  ├── LICENSE.md
  ├── Third Party Notices.md
  ├── QAReport.md
  ├── Editor
  │   ├── Unity.[YourPackageName].Editor.asmdef
  │   └── EditorExample.cs
  ├── Runtime
  │   ├── Unity.[YourPackageName].asmdef
  │   └── RuntimeExample.cs
  ├── Tests
  │   ├── .tests.json
  │   ├── Editor
  │   │   ├── Unity.[YourPackageName].Editor.Tests.asmdef
  │   │   └── EditorExampleTest.cs
  │   └── Runtime
  │        ├── Unity.[YourPackageName].Tests.asmdef
  │        └── RuntimeExampleTest.cs
  ├── Samples
  │   └── Example
  │       ├── .sample.json
  │       └── SampleExample.cs
  └── Documentation~
       ├── your-package-name.md
       └── Images
```

## Develop your package
Package development works best within the Unity Editor.  Here's how to set that up:

1. Clone the `Package Starter Kit` repository locally

    - In a console (or terminal) application, choose a place to clone the repository and perform the following :
    ```
    git clone git@github.cds.internal.unity3d.com:unity/com.unity.package-starter-kit.git
    ```

1. Create a new repository for your package and clone to your desktop

    - On Github.cds create a new repository with the name of your package (Example: `"com.unity.terrain-builder"`)
    - In a console (or terminal) application, choose a place to clone the repository and perform the following :
    ```
    git clone git@github.cds.internal.unity3d.com:unity/com.unity.[your-package-name]
    ```

1. Copy the contents of the Package Starter Kit folder to your new package.  Be careful not to copy the Package Starter Kit `.git` folder over.

1. #### Fill in your package information

	Update the following required fields in file **package.json** ([more info here](https://confluence.hq.unity3d.com/pages/viewpage.action?pageId=39065257)):
	- `"name"`: Package name, it should follow this naming convention: `"com.unity.[your-package-name]"`
	(Example: `"com.unity.2d.animation"`), no capital letters
	- `"displayName"`: Package user friendly display name. (Example: `"Terrain Builder SDK"`). <br>__Note:__ Use a display name that will help users understand what your package is intended for.
	- `"version"`: Package version `"X.Y.Z"`, your project **must** adhere to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).
		Follow this guideline:
		- To introduce a breaking API change, increment the major version (**X**.Y.Z)
		- To introduce a new feature, increment the minor version (X.**Y**.Z)
		- To introduce a bug fix, increment the patch version (X.Y.**Z**)
	- `"unity"`: Unity Version your package is compatible with. (Example: `"2018.1"`)
	- `"unityRelease"`: Optional, specify a patch release your package is compatible with (Example: `"0a8"`)
	- `"description"`: This description appears in the Package Manager window when the user selects this package from the list. For best results, use this text to summarize what the package does and how it can benefit the user.<br>__Note:__ Special formatting characters are supported, including line breaks (`\n`) and unicode characters such as bullets (`\u25AA`).<br>For more information, see the [Writing Package docs](https://confluence.hq.unity3d.com/display/DOCS/Writing+Package+docs) page on Confluence.

	Update the following recommended fields in file **package.json**:
	- `"dependencies"`: List of packages this package depends on.  All dependencies will also be downloaded and loaded in a project with your package.  Here's an example:
        ```
        dependencies: {
          "com.unity.ads": "1.0.0"
          "com.unity.analytics": "2.0.0"
        }
        ```

	- `"keywords"`: An array of keywords related to the package. This field is currently purely informational.
	
	- `"type"`: The type of your package. This is used to determine the visibility of your package in the Project Browser and the visibility of its Assets in the Object Picker. The `"tool"` and `"library"` types are used to set your package and its Assets as hidden by default. If not present or set to another value, your package and its Assets are visible by default.

	- `"hideInEditor"`: A boolean value that overrides the package visibility set by the package type. If set to `false`, the default value, your package and its Assets are **always** visible by default; if set to `true`, your package and its Assets are **always** hidden by default.

	**Note**:
	- For packages in development, neither `"type"` nor `"hideInEditor"` are used. The package is **always** visible in the Project Browser and its Assets are **always** visible in the Object Picker.
	- The user is **always** able to toggle the packages visibility in the Project Browser, as well as their Assets visibility in the Object Picker.

	Update the following field in file **Tests/.tests.json**:
	- `"createSeparatePackage"`: If this is set to true, the CI will create a separate package for these tests. If you leave it set to false, the tests will remain part of the published package. If you set it to true, the tests in your package will automatically be moved to a separate package, and metadata will be added at publish time to link the packages together. This allows you to have a large number of tests, or assets, etc. that you don't want to include in your main package, while making it easy to test your package with those tests & fixtures.

1. Start **Unity**, create a local empty project and import your package in the project

1. In a console (or terminal) application, push the template files you copied in your new package repository to it's remote
    - Add them to your repository's list to version
    ```git add .```
    - Commit to your new package's remote master
    ```git commit```
    - Push to your new package's remote master
    ```git push```

1. Restart Unity. For more information on embedded packages see [here](https://confluence.hq.unity3d.com/display/PAK/How+to+embed+a+package+in+your+project).

1. If on 2018.1 - Enable package support in the editor (*Internal Feature*).  From the **Project** window's right hand menu, enable `DEVELOPER`->`Show Packages in Project Window` (*only available in developer builds*).  You should now see your package in the Project Window, along with all other available packages for your project.

1. ##### Update **README.md**

    The README.md file should contain all pertinent information for developers using your package, such as:
	* Prerequistes
	* External tools or development libraries
	* Required installed Software
	* Command line examples to build, test, and run your package.

1. ##### Rename and update **your-package-name.md** documentation file.

    Use this template to create preliminary, high-level documentation. This document is meant to introduce users to the features and sample files included in your package.

1. ##### Rename and update assembly definition files.

	If your package contains Editor code, rename and modify [Editor/Unity.YourPackageName.Editor.asmdef](Editor/Unity.YourPackageName.Editor.asmdef). Otherwise, delete the Editor directory.
	* Name **must** match your package name, suffixed by `.Editor` (i.e `Unity.[YourPackageName].Editor`)
	* Assembly **must** reference `Unity.[YourPackageName]` (if you have any Runtime)
	* Platforms **must** include `"Editor"`


	If your package contains code that needs to be included in Unity runtime builds, rename and modify [Runtime/Unity.YourPackageName.asmdef](Runtime/Unity.YourPackageName.asmdef). Otherwise, delete the Runtime directory.
	* Name **must** match your package name (i.e `Unity.[YourPackageName]`)

	If your package has Editor code, you **must** have Editor Tests. In that case, rename and modify [Tests/Editor/Unity.YourPackageName.Editor.Tests.asmdef](Tests/Editor/Unity.YourPackageName.Editor.Tests.asmdef).
	* Name **must** match your package name, suffixed by `.Editor.Tests` (i.e `Unity.[YourPackageName].Editor.Tests`)
	* Assembly **must** reference `Unity.[YourPackageName].Editor` and `Unity.[YourPackageName]` (if you have any Runtime)
	* Platforms **must** include `"Editor"`
	* Optional Unity references **must** include `"TestAssemblies"` to allow your Editor Tests to show up in the Test Runner/run on Katana when your package is listed in project manifest `testables`

	If your package has Runtime code, you **must** have Playmode Tests. In that case, rename and modify [Tests/Runtime/Unity.YourPackageName.Tests.asmdef](Tests/Runtime/Unity.YourPackageName.Tests.asmdef).
	* Name **must** match your package name, suffixed by `.Tests` (i.e `Unity.[YourPackageName].Tests`)
	* Assembly **must** reference `Unity.[YourPackageName]`
	* Optional Unity references **must** include `"TestAssemblies"` to allow your Playmode Tests to show up in the Test Runner/run on Katana when your package is listed in project manifest `testables`

    >
    >  The reason for choosing such name schema is to ensure that the name of the assembly built based on *assembly definition file* (_a.k.a .asmdef_) will follow the .Net [Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/index)

1. ##### Document your package.

	**Document your public APIs**
	* All public APIs need to be documented with XmlDoc.  If you don't need an API to be accessed by clients, mark it as internal instead.
	* API documentation is generated from [XmlDoc tags](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments) included with all public APIs found in the package. See [Editor/EditorExample.cs](Editor/EditorExample.cs) for an example.

	**Document your features**
    * All packages that expose UI in the editor or runtime features should use the documentation template in [Documentation/your-package-name.md](Documentation/your-package-name.md).

	**Documentation flow**
	* Documentation needs to be ready when a publish request is sent to Release Management, as they will ask the documentation team to review it.
	* The package will remain in `preview` mode until the final documentation is completed.  Users will have access to the developer-generated documentation only in preview packages.
	* When the documentation is completed, the documentation team will update the package git repo with the updates and they will publish it on the web.
	* The package's development team will then need to submit a new package version with updated docs.
	* The starting page in the user manual that links to package documentation is [Here](http://docs.hq.unity3d.com/2018.1/Documentation/Manual/PackagesList.html).
	* The `Documentation~` folder is suffixed with `~` so that its content does not get loaded in the editor, which is the recommended behavior. `.Documentation` is also supported.
	* Your package should include the docs source in your package, but not the generated html. API docs are written as in-line XML tagged comments in the .cs files, besides the .md files for the Manual.

    **Test your documentation locally**
    As you are developing your documentation, you can see what your documentation will look like by using the DocTools package: com.unity.package-manager-doctools (optional).
    Once the DocTools package is installed, it will add a `Generate Documentation` button in the Package Manager UI's details of your installed packages. To install the extension, follow these steps:

    1. Make sure you have `Package Manager UI v1.9.6` or above.
    1. Your project manifest will need to point to a Candidates registry for this, which you can do by adding this line to it: `"registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"`
    1. Install `Package Manager DocTools v1.0.0-preview.6` or above from the `Package Manager UI` (in the `All Packages` section).
    1. After installation, you will see a `Generate Documentation` button which will generate the documentation locally, and open a web browser to a locally served version of your documentation so you can preview it.
    1. (optional) If your package documentation contains multiple `.md` files for the user manual, see [this page](https://docs.unity3d.com/Packages/com.unity.package-manager-doctools@1.0/manual/index.html#table-of-content) to add a table of content to your documentation.

    The DocTools extension is still in preview, if you come across arguable results, please discuss them on #docs-packman.

1. ##### Add samples to your package (code & assets).
    If your package contains a sample, rename the `Samples/Example` folder, and update the `.sample.json` file in it.

    In the case where your package contains multiple samples, you can make a copy of the `Samples/Example` folder for each sample, and update the `.sample.json` file accordingly.

    Delete the `Samples` folder altogether if your package does not need samples.

    As of Unity release 2019.1, the /Samples directory of a package will be recognized by the package manager.  Samples will not be imported to Unity when the package is added to a project, but will instead be offered to users of the package as an optional import, which can be added to their "/Assets" directory  through a UI option.

1. ##### Validate your package.

    **Validate your package using the Validation Suite**
    Before you publish your package, you need to make sure that it passes all the necessary validation checks by using the [Package Validation Suite extension](https://github.cds.internal.unity3d.com/unity/com.unity.package-validation-suite) (required).
    Once the Validation Suite package is installed, it will add a `Validate` button in the Package Manager UI's details of your installed packages. To install the extension, follow these steps:

    1. Make sure you have `Package Manager UI v1.9.6` or above.
    1. Your project manifest will need to point to the Candidates registry for this, which you can do by adding this line to it: `"registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"`
    1. Install the latest version of the `Package Validation Suite` from the `Package Manager UI` in the `All Packages` section. If you can't find it there, try turning on `Show preview packages` in the `Advanced` menu.
    1. After installation, you will see a `Validate` button show up in the Package Manager UI, which, when pressed, will run a series of tests and expose a `See Results` button for additional explanation.
        1. If it succeeds, you will see a green bar with a `Success` message.
        1. If it fails, you will see a red bar with a `Failed` message.

    The validation suite is still in preview, if you come across arguable results, please discuss them on #devs-packman.

1. ##### Design Guidelines.

    1. You should follow these design guideline when creating your package
      1. [Package design guidelines](https://confluence.hq.unity3d.com/display/UX/Packages)
      1. [Unity design checklist](https://unitytech.github.io/unityeditor-hig/topics/checklist.html)

    1. The namespace for code in the asmdef **must** match the asmdef name, except the initial `Unity`, which should be replaced with `UnityEngine` or `UnityEditor`:
      1. **Runtime code** We should only use the `Unity` namespace for code that has no dependency on anything in `UnityEngine` or `UnityEditor` and instead uses `ECS` and other `Unity`-namespace systems.

1. ##### Add tests to your package.

	**Editor tests**
	* Write all your Editor Tests in `Tests/Editor`
	* If your tests require access to internal methods, add an `AssemblyInfo.cs` file to your `Editor` code and use `[assembly: InternalsVisibleTo("Unity.[YourPackageName].Editor.Tests")]`

	**Playmode Tests**
	* Write all your Playmode Tests in `Tests/Runtime`.
	* If your tests require access to internal methods, add an `AssemblyInfo.cs` file to your `Runtime` code and use `[assembly: InternalsVisibleTo("Unity.[YourPackageName].Tests")]`

1. ##### Setup your package CI.

	Make sure your package continues to work against trunk or any other branch by setting up automated testing on every commit. [CI Setup Confluence page](https://confluence.hq.unity3d.com/display/PAK/Setting+up+your+package+CI).
    This starter kit contains the minimum recommended workflow for package CI, which provides the barebones to: `pack`, `test` and `publish` your packages. It also contains the required configuration to `promote` your **preview** packages to production.

1. ##### Update **CHANGELOG.md**.

	Every new feature or bug fix should have a trace in this file. For more details on the chosen changelog format, see [Keep a Changelog](http://keepachangelog.com/en/1.0.0/).

## Create a Pre-Release Package
Pre-Release Packages are a great way of getting your features in front of Unity Developers in order to get early feedback on functionality and UI designs.  Pre-Release packages need to go through the publishing to production flow, as would any other package, but with diminished requirements.  The only supported Pre-Release tag is `preview` (to be used in package.json,`version` field):

**Preview**  -  ex: `"version" : "1.2.0-preview"`
  * Expected Package structure respected
  * Package loads in Unity Editor without errors
  * License file present - With third party notices file if necessary
  * Test coverage is good - Optional but preferred
  * Public APIs documented, minimal feature docs exists- Optional but preferred

## Register your package

If you think you are working on a feature that is a good package candidate, please take a minute to fill-in this form: https://docs.google.com/forms/d/e/1FAIpQLSedxgDcIyf1oPyhWegp5FBvMm63MGAopeJhHDT5bU_BkFPNIQ/viewform?usp=sf_link.

Working with the board of dev directors and with product management, we will schedule the entry of the candidates in the ecosystem, based on technical challenges and on our feature roadmap.
Don’t hesitate to reach out and join us on **#devs-packman** on Slack.

## Share your package

If you want to share your project with other developers, the steps are similar to what's presented above. On the other developer's machine:

1. Start **Unity**, create a local empty project.

1. Launch console (or terminal) application, go to the newly created project folder, then clone your repository in the `Packages` directory

    ```none
    cd <YourProjectPath>/Packages
    git clone https://github.cdsinternal.unity3d.com/unity/[your-package-name].git com.unity.[sub-group].[your-package-name]
    ```
    __Note:__ Your directory name must be the name of your package (Example: `"com.unity.terrain-builder"`)

## Make sure your package meets all legal requirements

All packages are *required* to COMPLETE AND SUBMIT [THIS FORM](https://docs.google.com/forms/d/e/1FAIpQLSe3H6PARLPIkWVjdB_zMvuIuIVtrqNiGlEt1yshkMCmCMirvA/viewform) to receive approval. It is a simple, streamlined form that tells legal if there are any potential issues that need to be addressed prior to publication.

##### Update **Third Party Notices.md**

1. If your package has third-party elements and its licenses are approved, then all the licenses must be added to the `Third Party Notices.md` file. Simply duplicate the `Component Name/License Type/Provide License Details` section if you have more then one licenses.

    a. Concerning `[Provide License Details]` in the `Third Party Notices.md`, a URL can work as long as it actually points to the reproduced license and the copyright information _(if applicable)_.

1. If your package does not have third party elements, you can remove the `Third Party Notices.md` file from your package.

## Preparing your package for the Candidates registry

Before publishing your package to production, you must send your package on the Package Manager's internal **candidates** repository.  The candidates repository is monitored by QA and release management, and is where package validation will take place before it is accepted in production.

1. Publishing your changes to the package manager's **Candidates** registry happens from Github.cds.  To do so, simply setup your project's Continuous integration, which will be triggered by "Tags" on your branches.

For information please see [here](https://confluence.hq.unity3d.com/display/PAK/Setting+up+your+package+CI)


1. Test your package locally

    Now that your package is published on the package manager's **Candidates** registry, you can test your package in the editor by creating a new project, and editing the project's `manifest.json` file to point to your Candidate package, as such:
      ```
      dependencies: {
        "com.unity.[sub-group].[your-package-name]": "0.1.0"
      },
      "registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"
      ```

## Get your package published to Production

Packages are promoted to the **production** registry from **Candidates**, described above. Certain criteria must be met before submitting a request to promote a package to production.
[The list of criteria can be found here](https://docs.google.com/document/d/1TSnlSKJ6_h0C-CYO2LvV0fyGxJvH6OxC2-heyN8o-Gw/edit#heading=h.xxfb5jk2jda2)

Once you feel comfortable that your package meets the list of Release Management Criteria, [Submit your package publishing request to Release Management](https://docs.google.com/forms/d/e/1FAIpQLSdSIRO6s6_gM-BxXbDtdzIej-Hhk-3n68xSyC2sM8tp7413mw/viewform).

**Release management will validate your package content, and check that the editor/playmode tests are passed before promoting the package to production.  You will receive a confirmation email once the package is in production.**

**You're not done!**
At this point, your package is available on the cloud, 2 more steps are required to make your package discoverable in the editor:

1. Contact the Package Manager team in #devs-packman to ask them to add your package to the list of discoverable package for the Unity Editor.  All you need to provide is the package name (com.unity.[sub-group].[your-package-name])
1. If your package is meant to ship with a release of the editor (`Verified Packages` and `Bundled Packages`), follow these steps:
	* To be marked as verified, in trunk, modify the editor manifest ``[root]\External\PackageManager\Editor\manifest.json`` to include your package in the `recommended` list.
    * If your package is not verified, but only bundled with the editor, submit one or more Test Project(s) in Ono, so that your new package can be tested in all ABVs moving forward.  The following steps will create a test project that will run in ABVs, load your package into the project, and run all the tests found in your package.  The better your test coverage, the more confident you'll be that your package works with trunk.
    	* Create a branch in Ono, based on the latest branch this package must be compatible with (trunk, or release branch)
    	* If your package contains **Editor Tests**:
    		* In ``[root]\Tests\Editor.Tests``, create a new EditorTest Project (for new packages use **YourPackageName**) or use an existing project (for new versions of existing package).
    		* A [skeleton of EditorTest Project can be found here](https://oc.unity3d.com/index.php/s/Cldvuy6NpxqYy8y).
    		* Modify the project’s manifest.json file to include the production version of the package (name@version).
    		* Your project's manifest.json file should contain the following line: ``"testables" : [ "com.unity.[sub-group].[your-package-name]" ]``
    	* If your package contains **PlaymodeTests**:
    		* In ``[root]\Tests\PlaymodeTests``, create a new PlaymodeTest Project (for new packages use **YourPackageName**) or use an existing project (for new versions of existing package).
    		* Modify the project’s manifest.json file to include the candidate version of the package (name@version).
    		* Your project's manifest.json file should contain the following line: ``"testables" : [ "com.unity.[sub-group].[your-package-name]" ]``.
    		* Commit your branch changes to Ono, and run all Windows & Mac Editor/PlayMode tests (not full ABV) in Katana.
    * Once the tests are green on Katana, create your PR, add both `Latest Release Manager` and  `Trunk Merge Queue` as reviewers.


## FAQ

**What’s the difference between a preview package and a verified package?**

A preview package is a great way to develop and get feedback on new features and functionality.  Preview package can be created against any version of Unity 2018.1+, and can be made discoverable through the Package Manager UI by issuing a request in #devs-packman.  Quality and release schedule is up to the package owner, although minimum bars are set in place to ensure the package contains the right licenses, documentation, and a comprehensive set of tests.

Once a preview package has been in the field for 2-3 release cycles of the editor, that package can be considered for Verification. Verified packages are tested with a specific version of the editor, and offer our users a compatibility guarantee.  Verified packages are the only packages that can be included in the set of templates we ship with the editor (Verified Templates).  Code for these packages must follow core development guidelines, including code cutoff dates, and are tested in katana for continued compatibility.

**What’s the difference between a core package and a default package?**

A core package is a package that has its code included with the Editor’s core code.  This is interesting for packages that plan to change enormously in parallel to editor APIs.  By moving package code to the editor’s repo, both core API\functionality changes can be made along with required packages changes in the same PR.
https://docs.google.com/document/d/1CMoanjR3KAdew-6n39JdCFmHkTp1oshs3vkpejapf4Q/edit

A default package is a verified package that gets installed with every new project users create, regardless of the template they use.  We should limit the number of default packages we support, as each default package adds to the project loading time.  The list of default packages can be found in the editor manifest (https://ono.unity3d.com/unity/unity/files/de904b9ed9b44580ecd1e883f510daaa08182cc5/External/PackageManager/Editor/manifest.json).

**What are the requirement for me to publish a preview package?**

https://docs.google.com/document/d/1epGkAJRayJLN89_weA_-G5LFT_1uFifFZqBzAgvp_Zs/


**What are the requirements for me to get my package verified for a version of unity?**

https://docs.google.com/document/d/1oWC9XArVfkGMnqN9azR4hW4Pcd7-kQQw8Oy7ckP43JE/

**How is my verified package tested in Katana?**

https://docs.google.com/document/d/1jwTh71ZGtB2vF0SsHEwivt2FunaJWMGDdQJTpYRj3EE/edit

**How is my template tested in Katana?**

https://docs.google.com/document/d/1jwTh71ZGtB2vF0SsHEwivt2FunaJWMGDdQJTpYRj3EE/edit

**How do I add samples to my package?**

https://docs.google.com/document/d/1rmxGh6Z9gtbQlGUKCsVBaR0RyHvzq_gsWoYs6sttzYA/edit#heading=h.fg1e3sz56048

**How do I setup CI or publishing options for my package?**
https://confluence.hq.unity3d.com/display/PAK/Setting+up+your+package+CI

**How can I add tests to my package?**

There’s a “Tests” directory in the package starter kit.  If you add editor and playmode tests in that directory, they will make up the list of tests for your package.

**The tests in my package bloat my package too much, what are my options?**

https://docs.google.com/document/d/19kKIGFetde5ES-gKXQp_P7bxQ9UgBnBUu58-y7c1rTA/edit

**Can I automate my package publishing yet?**

Not just yet, but we’re working on it.  The first automated publishing we will enable is the push to production for preview packages.  Basically, when your package passes validation (loads in the editor without error, the tests in the package pass, validation suite run success), the package will be pushed to production automatically.  Other publishing flows will soon be available as well, here’s the full list of internal package publishing flows Unity will support.  https://docs.google.com/document/d/1zdMzAtfi-vgM8NMPmwL40yinBeL3YImwTO5gSfGNCgs/edit

**How do I get a template package started?**

Start with the Project Template Starter Kit (you can request access in #devs-packman).
https://github.cds.internal.unity3d.com/unity/com.unity.template-starter-kit

**How do I get my package included in a template?**

First and foremost, your package needs to be on the verified list of packages.  Only verified packages can get added to templates we ship with the editor.  Then reach out to the templates community in #devs-template to open discussions on adding your package to one or more of our existing templates.

**How can I test my package locally, as a user would?**

https://confluence.hq.unity3d.com/display/PAK/How+to+add+a+git+package+to+your+project

**What tests are included by the validation suite?**

https://docs.google.com/spreadsheets/d/1CdO7D0WSirbZhjnVsdJxJwOPK4UdUDxSRBIqwyjm70w/edit#gid=0
