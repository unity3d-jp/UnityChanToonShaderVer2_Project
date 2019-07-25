>>>
**_Package Documentation Template_**

Use this template to create preliminary, high-level documentation meant to introduce users to the feature and the sample files included in this package. When writing your documentation, do the following:

1. Follow instructions in blockquotes.

2. Replace angle brackets with the appropriate text. For example, replace "&lt;package name&gt;" with the official name of the package.
 
3. Delete sections that do not apply to your package. For example, a package containing only sample files does not have a "Using &lt;package_name&gt;" section, so this section can be removed.
 
4. After documentation is completed, make sure you delete all instructions and examples in blockquotes including this preamble and its title:

		```
		>>>
		Delete all of the text between pairs of blockquote markdown.
		>>>
		```
>>>

# About &lt;package name&gt;

>>>
Name the heading of the first topic after the **displayName** of the package as it appears in the package manifest. Check with your Product Manager to ensure that the package is named correctly. 

This first topic includes a brief, high-level explanation of the package and, if applicable, provides links to Unity Manual topics.

There are two types of packages:

 - Packages that include features that augment the Unity Editor or Runtime.
 - Packages that include sample files.

Choose one of the following introductory paragraphs that best fits the package:
>>>

Use the &lt;package name&gt; package to &lt;list of the main uses for the package&gt;. For example, use &lt;package name&gt; to create/generate/extend/capture &lt;mention major use case, or a good example of what the package can be used for&gt;. The &lt;package name&gt; package also includes &lt;other relevant features or uses&gt;.

> *or*

The &lt;package name&gt; package includes examples of &lt;name of asset type, model, prefabs, and/or other GameObjects in the package&gt;. For more information, see &lt;xref to topic in the Unity Manual&gt;.

>>>
**_Examples:_** 

Here are some examples for reference only. Do not include these in the final documentation file:

*Use the Unity Recorder package to capture and save in-game data. For example, use Unity Recorder to record an mp4 file during a game session. The Unity Recorder package also includes an interface for setting-up and triggering recording sessions.*

*The Timeline Examples package includes examples of Timeline assets, Timeline Instances, animation, GameObjects, and scripts that illustrate how to use Unity's Timeline. For more information, see [ Unity's Timeline](https://docs.unity3d.com/Manual/TimelineSection.html) in the [Unity Manual](https://docs.unity3d.com). For licensing and usage, see Package Licensing.*
>>>

# Installing &lt;package name&gt;
>>>
Begin this section with a cross-reference to the official Unity Manual topic on how to install packages. If the package requires special installation instructions, include these steps in this section.
>>>

To install this package, follow the instructions in the [Package Manager documentation](https://docs.unity3d.com/Packages/com.unity.package-manager-ui@latest/index.html). 

>>>
For some packages, there may be additional steps to complete the setup. You can add those here.
>>>

In addition, you need to install the following resources:

 - &lt;name of resource&gt;: To install, open *Window > &lt;name of menu item&gt;*. The resource appears &lt;at this location&gt;.
 - &lt;name of sample&gt;: To install, open *Window > &lt;name of menu item&gt;*. The new sample folder appears &lt;at this location&gt;.


<a name="UsingPackageName"></a>
# Using &lt;package name&gt;
>>>
The contents of this section depends on the type of package.

For packages that augment the Unity Editor with additional features, this section should include workflow and/or reference documentation:

* At a minimum, this section should include reference documentation that describes the windows, editors, and properties that the package adds to Unity. This reference documentation should include screen grabs (see how to add screens below), a list of settings, an explanation of what each setting does, and the default values of each setting.
* Ideally, this section should also include a workflow: a list of steps that the user can easily follow that demonstrates how to use the feature. This list of steps should include screen grabs (see how to add screens below) to better describe how to use the feature.

For packages that include sample files, this section may include detailed information on how the user can use these sample files in their projects and scenes. However, workflow diagrams or illustrations could be included if deemed appropriate.

## How to add images

*(This section is for reference. Do not include in the final documentation file)* 

If the [Using &lt;package name&gt;](#UsingPackageName) section includes screen grabs or diagrams, a link to the image must be added to this MD file, before or after the paragraph with the instruction or description that references the image. In addition, a caption should be added to the image link that includes the name of the screen or diagram. All images must be PNG files with underscores for spaces. No animated GIFs.

An example is included below:

![A cinematic in the Timeline Editor window.](images/example.png)

Notice that the example screen shot is included in the images folder. All screen grabs and/or diagrams must be added and referenced from the images folder.

For more on the Unity documentation standards for creating and adding screen grabs, see this confluence page: https://confluence.hq.unity3d.com/pages/viewpage.action?pageId=13500715
>>>



# Technical details
## Requirements
>>>
This subtopic includes a bullet list with the compatible versions of Unity. This subtopic may also include additional requirements or recommendations for 3rd party software or hardware. An example includes a dependency on other packages. If you need to include references to non-Unity products, make sure you refer to these products correctly and that all references include the proper trademarks (tm or r)
>>>

This version of &lt;package name&gt; is compatible with the following versions of the Unity Editor:

* 2018.1 and later (recommended)

To use this package, you must have the following 3rd party products:

* &lt;product name and version with trademark or registered trademark.&gt;
* &lt;product name and version with trademark or registered trademark.&gt;
* &lt;product name and version with trademark or registered trademark.&gt;

## Known limitations
>>>
This section lists the known limitations with this version of the package. If there are no known limitations, or if the limitations are trivial, exclude this section. An example is provided.
>>>

&lt;package name&gt; version &lt;package version&gt; includes the following known limitations:

* &lt;brief one-line description of first limitation.&gt;
* &lt;brief one-line description of second limitation.&gt;
* &lt;and so on&gt;

>>>
*Example (For reference. Do not include in the final documentation file):*

The Unity Recorder version 1.0 has the following limitations:*

* The Unity Recorder does not support sound.
* The Recorder window and Recorder properties are not available in standalone players.
* MP4 encoding is only available on Windows.
>>>

## Package contents
>>>
This section includes the location of important files you want the user to know about. For example, if this is a sample package containing textures, models, and materials separated by sample group, you may want to provide the folder location of each group.
>>>

The following table indicates the &lt;describe the breakdown you used here&gt;:

|Location|Description|
|---|---|
|`<folder>`|Contains &lt;describe what the folder contains&gt;.|
|`<file>`|Contains &lt;describe what the file represents or implements&gt;.|

>>>
*Example (For reference. Do not include in the final documentation file):*

The following table indicates the root folder of each type of sample in this package. Each sample's root folder contains its own Materials, Models, or Textures folders:

|Folder Location|Description|
|---|---|
|`WoodenCrate_Orange`|Root folder containing the assets for the orange crates.|
|`WoodenCrate_Mahogany`|Root folder containing the assets for the mahogany crates.|
|`WoodenCrate_Shared`|Root folder containing any material assets shared by all crates.|
>>>

## Document revision history
>>>
This section includes the revision history of the document. The revision history tracks when a document is created, edited, and updated. If you create or update a document, you must add a new row describing the revision.  The Documentation Team also uses this table to track when a document is edited and its editing level. An example is provided:
 
|Date|Reason|
|---|---|
|Sept 12, 2017|Unedited. Published to package.|
|Sept 10, 2017|Document updated for package version 1.1.<br>New features: <li>audio support for capturing MP4s.<li>Instructions on saving Recorder prefabs|
|Sept 5, 2017|Limited edit by Documentation Team. Published to package.|
|Aug 25, 2017|Document created. Matches package version 1.0.|
>>>