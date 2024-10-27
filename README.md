# 主题支持（Theme Support）- CSTI MOD



## 简介

主题支持（Theme Support）是一个功能类Mod，为Mod作者提供更换游戏主题风格的功能，如游戏封面、背景等。



当前版本：1.1.1

By.サトシの皮卡丘



## 安装说明

需前置Mod：ModLoader v2.0.1+

请将Mod压缩包解压于 BepInEx\plugins 文件夹下。



## 使用说明

Tips：该内容供Mod作者阅读。

Tips：以下内容除 “本Mod” 的表述是指 “主题支持” Mod外，其他 “Mod” 的表述皆指您所制作的 Mod。

**Tips：玩家需安装本Mod，才能使您的主题在游戏中生效！**



### 安装 ModEditor-JsonData

将**本Mod**目录中的 “CSTI-JsonData” 文件夹复制到 ModEditor 的根目录中，即可使 ModEditor 支持主题。



### 添加主题资源

1. 在 Mod 的 “Resource” 文件夹中创建名为 “ThemePicture” 的文件夹。
2. 将图片资源文件放入 “ThemePicture” 文件夹中。

Tips：资源的文件名是唯一标识，且与任何Mod共享，请注意防止命名冲突！



### 自定义封面

#### 创建封面主题

1. 在 Mod 的 “ScriptableObject” 文件夹中创建名为 “Theme-Cover” 的文件夹。
2. 在 ModEditor 中导入 Mod 项目。
3. 在左侧文件列表中找到并右击 “Theme-Cover” 文件夹，在弹出菜单中选择“新建文件”。
4. 在弹出窗口中输入创建的封面名称（无实际作用），选择模板，并点击 “OK” 按钮即可完成创建。

Tips：封面名称虽然无实际作用，但仍然是唯一标识，且与任何Mod共享，请注意防止命名冲突！



#### 设置封面内容

1. 在 ModEditor 中打开创建好的封面。
2. 点击“隐藏未激活属性“按钮。
5. 根据需求，填写 “CoverImageName”、“Title”、“BGM” 等字段。

Tips：各Mod的各个封面主题会在显示封面时随机选取一个。



### 自定义游戏背景

#### 创建游戏背景

1. 在 Mod 的 “ScriptableObject” 文件夹中创建名为 “Theme-GameBack” 的文件夹。
2. 在 ModEditor 中导入 Mod 项目。
3. 在左侧文件列表中找到并右击 “Theme-GameBack” 文件夹，在弹出菜单中选择“新建文件”。
4. 在弹出窗口中输入创建的背景名称（无实际作用），选择模板，并点击 “OK” 按钮即可完成创建。

Tips：封面名称虽然无实际作用，但仍然是唯一标识，且与任何Mod共享，请注意防止命名冲突！



#### 设置游戏背景

1. 在 ModEditor 中打开创建好的游戏背景。
2. 点击“隐藏未激活属性“按钮。
3. 根据需求，填写 “Card”、“XxxSet”、“ConditionSets” 等字段。



## 更新日志

### Version 1.1.1

修复在游戏背景完成第一次卡牌绑定时会导致绑定提前结束的问题。



### Version 1.1.0

新增自定义游戏背景功能