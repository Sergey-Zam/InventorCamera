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

        'заполнить текущий путь сохранения файлов
        tbSavePath.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If _invApp.Documents.Count = 0 Then 'если не открыто ни 1 документа
            MsgBox("В Inventor не открыто ни одного документа")
            Return 'выход из функции обработчика кнопки
        End If

        If Not (_invApp.ActiveDocument.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Or _invApp.ActiveDocument.DocumentType = DocumentTypeEnum.kPartDocumentObject) Then
            MsgBox("Требуется открыть документ сборки или детали")
            Return
        End If

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
        v.GoHome() 'установить на домашний вид

        'установить тему с белым фоном
        _invApp.ColorSchemes.Item("Presentation").Activate() 'this color scheme contains a white background
        _invApp.ColorSchemes.BackgroundType = BackgroundTypeEnum.kOneColorBackgroundType

        'инициализация камеры
        Dim camera As Camera = v.Camera 'camera.Eye.X or .Y or .Z ; camera.PerspectiveAngle - угол 

        'изменение видов камеры и получение снимков
        getStandardViews(v, camera) 'получить стандартно-доступные виды (все 6 граней и 4 Iso-вершины)
        If (cbOnlyStandardViews.Checked = False) Then
            getCustomViews(v, camera) 'получить кастомные виды (4 оставшихся Iso-вершины и 12 рёбер)
        End If

        MsgBox("Все снимки сделаны." & vbCrLf &
               "Расположение на диске: " & FolderBrowserDialog1.SelectedPath)
    End Sub

    Private Sub getStandardViews(v As View, camera As Camera)
        'установить вид камеры: Front - спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kFrontViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Front") 'сделать снимок экрана

        'установить вид камеры: Back - сзади
        camera.ViewOrientationType = ViewOrientationTypeEnum.kBackViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Back") 'сделать снимок экрана

        'установить вид камеры: Right - справа
        camera.ViewOrientationType = ViewOrientationTypeEnum.kRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Right") 'сделать снимок экрана

        'установить вид камеры: Left - слева
        camera.ViewOrientationType = ViewOrientationTypeEnum.kLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Left") 'сделать снимок экрана

        'установить вид камеры: Top - сверху
        camera.ViewOrientationType = ViewOrientationTypeEnum.kTopViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Top") 'сделать снимок экрана

        'установить вид камеры: Bottom - снизу
        camera.ViewOrientationType = ViewOrientationTypeEnum.kBottomViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "Bottom") 'сделать снимок экрана

        'установить вид камеры: IsoBottomLeft - снизу слева спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoBottomLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "IsoBottomLeft") 'сделать снимок экрана

        'установить вид камеры: IsoBottomRight - снизу справа спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoBottomRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "IsoBottomRight") 'сделать снимок экрана

        'установить вид камеры: IsoTopLeft - сверху слева спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "IsoTopLeft") 'сделать снимок экрана

        ''установить вид камеры: IsoTopRight - сверху справа спереди
        camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation
        camera.Fit()
        camera.Apply() 'view update too
        MakeScreen(v, camera, "IsoTopRight") 'сделать снимок экрана
    End Sub

    Private Sub getCustomViews(v As View, camera As Camera)
        Dim oTG As TransientGeometry = _invApp.TransientGeometry 'инициализация TransientGeometry

        ''установить кастомный вид камеры: IsoBottomLeftBack - снизу слева сзади
        Dim oEye As Inventor.Point = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        Dim oTarget As Inventor.Point = oTG.CreatePoint(1, 1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        Dim oUpVector As UnitVector = oTG.CreateUnitVector(1, 1, 0) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "IsoBottomLeftBack") 'сделать снимок экрана

        'установить кастомный вид камеры: IsoBottomRightBack - снизу справа сзади
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, 1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 0) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "IsoBottomRightBack") 'сделать снимок экрана

        'установить кастомный вид камеры: IsoTopLeftBack - сверху слева сзади
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(1, -1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 0) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "IsoTopLeftBack") 'сделать снимок экрана

        'установить кастомный вид камеры: IsoTopRightBack - сверху справа сзади
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, -1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 0) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "IsoTopRightBack") 'сделать снимок экрана

        '--далее кастомные виды на ребра--
        'установить вид на ребро: TopRight
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, -1, 0) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "TopRightEdge") 'сделать снимок экрана

        'установить вид на ребро: TopLeft
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(1, -1, 0) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "TopLeftEdge") 'сделать снимок экрана

        'установить вид на ребро: BottomRight
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, 1, 0) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "BottomRightEdge") 'сделать снимок экрана

        'установить вид на ребро: BottomLeft
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(1, 1, 0) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "BottomLeftEdge") 'сделать снимок экрана

        'установить вид на ребро: TopFront
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(0, -1, -1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "TopFrontEdge") 'сделать снимок экрана

        'установить вид на ребро: TopBack
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(0, -1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "TopBackEdge") 'сделать снимок экрана

        'установить вид на ребро: BottomFront
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(0, 1, -1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "BottomFrontEdge") 'сделать снимок экрана

        'установить вид на ребро: BottomBack
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(0, 1, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "BottomBackEdge") 'сделать снимок экрана

        'установить вид на ребро: RightFront
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, 0, -1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "RightFrontEdge") 'сделать снимок экрана

        'установить вид на ребро: RightBack
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(-1, 0, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "RightBackEdge") 'сделать снимок экрана

        'установить вид на ребро: LeftFront
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(1, 0, -1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "LeftFrontEdge") 'сделать снимок экрана

        'установить вид на ребро: LeftBack
        oEye = oTG.CreatePoint(0, 0, 0)
        camera.Eye = oEye 'set camera Eye
        oTarget = oTG.CreatePoint(1, 0, 1) 'Определяет положение целевой точки, которую наблюдатель просматривает на сцене (ось Z вида).
        camera.Target = oTarget 'set camera Target
        oUpVector = oTG.CreateUnitVector(1, 1, 1) 'Определяет вектор, определяющий, что «вверх» для наблюдателя.
        camera.UpVector = oUpVector 'set camera UpVector
        camera.Fit()
        camera.Apply() 'apply camera settings
        MakeScreen(v, camera, "LeftBackEdge") 'сделать снимок экрана
    End Sub

    Private Sub MakeScreen(v As View, camera As Camera, description As String)
        'Динамическое получение ширины и высоты вида. Стандарт: (1024, 768), но тогда возможны поетри части изображения
        Dim width = v.Width
        Dim height = v.Height
        Dim savePath = FolderBrowserDialog1.SelectedPath
        Dim name = description & " " & DateTime.Now.ToString("yyyy/MM/dd HH.mm.ss") & ".bmp"
        v.SaveAsBitmap(savePath & "\" & name, width, height)

        If (cbShowAllMessages.Checked = True) Then
            MsgBox("Screen is ready." & vbCrLf &
               "Location: " & savePath & vbCrLf &
               "Name: " & name & vbCrLf &
               "Width(px): " & width & vbCrLf &
               "Height(px): " & height & vbCrLf &
               "Camera" & vbCrLf &
               "X: " & camera.Eye.X & vbCrLf &
               "Y: " & camera.Eye.Y & vbCrLf &
               "Z: " & camera.Eye.Z)
        End If
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            tbSavePath.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnReadCoords_Click(sender As Object, e As EventArgs) Handles btnReadCoords.Click
        If _invApp.Documents.Count = 0 Then 'если не открыто ни 1 документа
            MsgBox("В Inventor не открыто ни одного документа")
            Return 'выход из функции обработчика кнопки
        End If

        If Not (_invApp.ActiveDocument.DocumentType = DocumentTypeEnum.kAssemblyDocumentObject Or _invApp.ActiveDocument.DocumentType = DocumentTypeEnum.kPartDocumentObject) Then
            MsgBox("Требуется открыть документ сборки или детали")
            Return
        End If

        'попытка открыть вид (на экране должна быть открыта деталь или сборка)
        Dim v As View
        Try
            v = _invApp.ActiveView
        Catch ex As Exception
            MsgBox("В Inventor должен быть открыт хотя бы один вид")
            Exit Sub
        End Try

        Dim camera As Camera = v.Camera 'camera.Eye.X or .Y or .Z ; camera.PerspectiveAngle - угол 

        MsgBox("X: " & camera.Eye.X & vbCrLf &
               "Y: " & camera.Eye.Y & vbCrLf &
               "Z: " & camera.Eye.Z)
    End Sub
End Class
