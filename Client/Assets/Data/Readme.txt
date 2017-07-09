这个目录是用来存放数据文件的。
1）目录说明：
Editor下是导出工具
Excels下存放Excel文件
DataDefine下存放数据定义文件（即ScriptableObject文件）
Datas下存放从Excel导出，并在Unity中使用的导出数据

2) 使用方法
Unity菜单Assets/Excel/All 将编译 Data/Excels 目录下的Excel，并把生成的数据存放在 Data/Datas下面； 现在只有Build数据。（注意，目前还未处理好，编译前请删除Build目录下的内容）

2）基本工作原理说明。
Excel用来编辑策划数据。通过工具结合数据定义文件（DataDefines），把Excel的内容导出为特定的数据，并存放在Datas目录下。
Datas下的文件不光包括Excel的数据，而且还包括数据的关系。

举例：
Role，Talent，Trap三张表，分别导出三种数据。这三种数据是有关系的，Role包含Talent，Talent可能包含Trap，Trap还可以包含Talent等。
这些关系，以前是通过ID来维持关系的。目前可以通过ScriptableObject的特性，在导出成数据之后再进行组装。

在导出数据之后，再对数据间关系进行处理，有很多好处：
1. 简化填表
2. Data阶段处理可以更加直观，减少错误
3. 对程序友好，方便编程，易于维护。包括初始化的复杂度，性能，内存占用，查错等都有帮助。



