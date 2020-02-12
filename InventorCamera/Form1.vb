Imports System
Imports System.Type
Imports System.Activator
Imports System.Runtime.InteropServices
Imports Inventor

Public Class Form1
    Dim _invApp As Inventor.Application 'переменная типа Inventor.Application Используется для доступа к объекту Inventor Application

    Public Sub New()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        'здесь размещается любой инициализирующий код.
        Try
            'пытаемся получить ссылку на запущенный Inventor
            _invApp = Marshal.GetActiveObject("Inventor.Application") 'После выполнения этой строки переменная _invApp будет содержать ссылку на объект типа Inventor.Application. Функция GetActiveObject помогает нам найти существующий сеанс Inventor. 
        Catch ex As Exception
            'если не удалось получить ссылку (например, Inventor не запущен), то код ниже попытается создать новый сеанс Inventor.
            Try
                Dim invAppType As Type = GetTypeFromProgID("Inventor.Application")
                _invApp = CreateInstance(invAppType)
                _invApp.Visible = True
            Catch ex2 As Exception
                MsgBox(ex2.ToString())
                MsgBox("Не удалось ни найти, ни создать сеанс Inventor")
            End Try
        End Try

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        TestInventorCamera()
    End Sub

    Public Sub TestInventorCamera()
        'попытка открыть вид (на экране должна быть открыта деталь или сборка)
        Dim v As View
        Try
            v = _invApp.ActiveView
        Catch ex As Exception
            MsgBox("В Inventor должен быть открыт хотя бы один вид")
            Exit Sub
        End Try

        'вид получен
        'установить домашний вид
        v.GoHome()

        'установить тему с белым фоном
        _invApp.ColorSchemes.Item("Presentation").Activate() 'this color scheme contains a white background
        _invApp.ColorSchemes.BackgroundType = BackgroundTypeEnum.kOneColorBackgroundType

        'инициализация камеры
        Dim camera As Camera = v.Camera 'camera.Eye.X or .Y or .Z ; camera.PerspectiveAngle - угол 

        'установить вид камеры: Front - спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kFrontViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Front") 'сделать снимок экрана

        'установить вид камеры: Back - сзади
        camera.ViewOrientationType = ViewOrientationTypeEnum.kBackViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Back") 'сделать снимок экрана

        'установить вид камеры: Right - справа
        camera.ViewOrientationType = ViewOrientationTypeEnum.kRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Right") 'сделать снимок экрана

        'установить вид камеры: Left - слева
        camera.ViewOrientationType = ViewOrientationTypeEnum.kLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Left") 'сделать снимок экрана

        'установить вид камеры: Top - сверху
        camera.ViewOrientationType = ViewOrientationTypeEnum.kTopViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Top") 'сделать снимок экрана

        'установить вид камеры: Bottom - снизу
        camera.ViewOrientationType = ViewOrientationTypeEnum.kBottomViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "Bottom") 'сделать снимок экрана

        'установить вид камеры: IsoBottomLeft - снизу слева спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoBottomLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "IsoBottomLeft") 'сделать снимок экрана

        'установить вид камеры: IsoBottomRight - снизу справа спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoBottomRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "IsoBottomRight") 'сделать снимок экрана

        'установить вид камеры: IsoTopLeft - сверху слева спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "IsoTopLeft") 'сделать снимок экрана

        'установить вид камеры: IsoTopRight - сверху справа спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, "IsoTopRight") 'сделать снимок экрана
    End Sub

    Private Sub MakeScreen(v As View, description As String)
        'Динамическое получение ширины и высоты вида. Стандарт: (1024, 768), но тогда возможны поетри части изображения
        Dim width = v.Width
        Dim height = v.Height
        Dim desctopPath = My.Computer.FileSystem.SpecialDirectories.Desktop
        Dim name = description & " " & DateTime.Now.ToString("yyyy/MM/dd HH.mm.ss") & ".bmp"
        v.SaveAsBitmap(desctopPath & "\" & name, width, height)
        MsgBox("Screen is ready." & vbCrLf &
               "Location: " & desctopPath & vbCrLf &
               "Name: " & name & vbCrLf &
               "Width(px): " & width & vbCrLf &
               "Height(px): " & height)
    End Sub
End Class
