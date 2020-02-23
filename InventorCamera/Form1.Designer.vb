<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tbSavePath = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReadCoords = New System.Windows.Forms.Button()
        Me.cbOnlyStandardViews = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbShowAllMessages = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(15, 102)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(369, 35)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Старт"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tbSavePath
        '
        Me.tbSavePath.Location = New System.Drawing.Point(15, 65)
        Me.tbSavePath.Name = "tbSavePath"
        Me.tbSavePath.ReadOnly = True
        Me.tbSavePath.Size = New System.Drawing.Size(288, 20)
        Me.tbSavePath.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(309, 63)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(75, 23)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "Обзор"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.SelectedPath = "C:\Users\Сергей\Desktop"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(283, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "1. Перед началом работы откройте сборку или деталь"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(259, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "2. Выберите директорию сохранения скриншотов"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 156)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Дополнительно"
        '
        'btnReadCoords
        '
        Me.btnReadCoords.Location = New System.Drawing.Point(15, 263)
        Me.btnReadCoords.Name = "btnReadCoords"
        Me.btnReadCoords.Size = New System.Drawing.Size(223, 23)
        Me.btnReadCoords.TabIndex = 6
        Me.btnReadCoords.Text = "Считать текущие координаты камеры"
        Me.btnReadCoords.UseVisualStyleBackColor = True
        '
        'cbOnlyStandardViews
        '
        Me.cbOnlyStandardViews.AutoSize = True
        Me.cbOnlyStandardViews.Location = New System.Drawing.Point(15, 181)
        Me.cbOnlyStandardViews.Name = "cbOnlyStandardViews"
        Me.cbOnlyStandardViews.Size = New System.Drawing.Size(248, 17)
        Me.cbOnlyStandardViews.TabIndex = 7
        Me.cbOnlyStandardViews.Text = "Делать снимки только стандартных* видов"
        Me.cbOnlyStandardViews.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 201)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(363, 26)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "*Стандартные виды включают 6 граней и 4 изометрических вершины." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Всегда корректно" &
    " отрабатывают для различных деталей/сборок."
        '
        'cbShowAllMessages
        '
        Me.cbShowAllMessages.AutoSize = True
        Me.cbShowAllMessages.Location = New System.Drawing.Point(15, 240)
        Me.cbShowAllMessages.Name = "cbShowAllMessages"
        Me.cbShowAllMessages.Size = New System.Drawing.Size(331, 17)
        Me.cbShowAllMessages.TabIndex = 9
        Me.cbShowAllMessages.Text = "Показывать сообщение после каждого сделанного снимка"
        Me.cbShowAllMessages.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 298)
        Me.Controls.Add(Me.cbShowAllMessages)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cbOnlyStandardViews)
        Me.Controls.Add(Me.btnReadCoords)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.tbSavePath)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "Form1"
        Me.Text = "InventorCamera"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents tbSavePath As TextBox
    Friend WithEvents btnBrowse As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnReadCoords As Button
    Friend WithEvents cbOnlyStandardViews As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cbShowAllMessages As CheckBox
End Class
