// <auto-generated/>
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
    private global::Gtk.HBox hbxMain;
    private global::Gtk.DrawingArea drawingArea;
    private global::Gtk.VBox vbxConfig;
    private global::Gtk.VBox vbxSample;
    private global::Gtk.Label label4;
    private global::Gtk.ComboBox cmbSample;
    private global::Gtk.VPaned problemConfigWidgetContainer;
    private global::Gtk.VBox vbxGA;
    private global::Gtk.VButtonBox vbbSample;
    private global::Gtk.HSeparator hseparator1;
    private global::Gtk.Label label1;
    private global::Gtk.HBox hbox2;
    private global::Gtk.Label label2;
    private global::Gtk.SpinButton sbtPopulationMinSize;
    private global::Gtk.HBox hbox3;
    private global::Gtk.Label label3;
    private global::Gtk.SpinButton sbtPopulationMaxSize;
    private global::Gtk.HBox hbox11;
    private global::Gtk.ComboBox cmbGenerationStrategy;
    private global::Gtk.Button btnEditGenerationStrategy;
    private global::Gtk.HSeparator hseparator2;
    private global::Gtk.Label label5;
    private global::Gtk.HBox hbox5;
    private global::Gtk.ComboBox cmbSelection;
    private global::Gtk.Button btnEditSelection;
    private global::Gtk.HSeparator hseparator4;
    private global::Gtk.Label label7;
    private global::Gtk.HBox hbox9;
    private global::Gtk.Label label10;
    private global::Gtk.HScale hslCrossoverProbability;
    private global::Gtk.HBox hbox6;
    private global::Gtk.ComboBox cmbCrossover;
    private global::Gtk.Button btnEditCrossover;
    private global::Gtk.HSeparator hseparator5;
    private global::Gtk.Label label8;
    private global::Gtk.HBox hbox8;
    private global::Gtk.Label label9;
    private global::Gtk.HScale hslMutationProbability;
    private global::Gtk.HBox hbox7;
    private global::Gtk.ComboBox cmbMutation;
    private global::Gtk.Button btnEditMutation;
    private global::Gtk.HSeparator hseparator3;
    private global::Gtk.Label label12;
    private global::Gtk.HBox hbox12;
    private global::Gtk.ComboBox cmbReinsertion;
    private global::Gtk.Button btnEditReinsertion;
    private global::Gtk.Label label11;
    private global::Gtk.HBox hbox10;
    private global::Gtk.ComboBox cmbTermination;
    private global::Gtk.Button btnEditTermination;
    private global::Gtk.HSeparator hseparator6;
    private global::Gtk.Button btnStart;
    private global::Gtk.Button btnNew;
    private global::Gtk.Button btnStop;
    private global::Gtk.Button btnResume;

    protected virtual void Build()
    {
        global::Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.Name = "MainWindow";
        this.Title = global::Mono.Unix.Catalog.GetString("GeneticSharp :: Runner");
        this.Icon = new global::Gdk.Pixbuf(global::System.IO.Path.Combine(global::System.AppDomain.CurrentDomain.BaseDirectory, "./Icon.png"));
        this.WindowPosition = ((global::Gtk.WindowPosition)(1));
        this.Modal = true;
        this.DefaultWidth = 800;
        this.DefaultHeight = 600;
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.hbxMain = new global::Gtk.HBox();
        this.hbxMain.Name = "hbxMain";
        this.hbxMain.Spacing = 10;
        this.hbxMain.BorderWidth = ((uint)(10));
        // Container child hbxMain.Gtk.Box+BoxChild
        this.drawingArea = new global::Gtk.DrawingArea();
        this.drawingArea.Name = "drawingArea";
        this.hbxMain.Add(this.drawingArea);
        global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbxMain[this.drawingArea]));
        w1.Position = 0;
        w1.Padding = ((uint)(1));
        // Container child hbxMain.Gtk.Box+BoxChild
        this.vbxConfig = new global::Gtk.VBox();
        this.vbxConfig.Name = "vbxConfig";
        this.vbxConfig.Spacing = 6;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.vbxSample = new global::Gtk.VBox();
        this.vbxSample.Name = "vbxSample";
        this.vbxSample.Spacing = 6;
        // Container child vbxSample.Gtk.Box+BoxChild
        this.label4 = new global::Gtk.Label();
        this.label4.Name = "label4";
        this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Problem");
        this.vbxSample.Add(this.label4);
        global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbxSample[this.label4]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child vbxSample.Gtk.Box+BoxChild
        this.cmbSample = global::Gtk.ComboBox.NewText();
        this.cmbSample.Name = "cmbSample";
        this.vbxSample.Add(this.cmbSample);
        global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbxSample[this.cmbSample]));
        w3.Position = 1;
        w3.Expand = false;
        w3.Fill = false;
        // Container child vbxSample.Gtk.Box+BoxChild
        this.problemConfigWidgetContainer = new global::Gtk.VPaned();
        this.problemConfigWidgetContainer.CanFocus = true;
        this.problemConfigWidgetContainer.Name = "problemConfigWidgetContainer";
        this.problemConfigWidgetContainer.Position = 10;
        this.vbxSample.Add(this.problemConfigWidgetContainer);
        global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbxSample[this.problemConfigWidgetContainer]));
        w4.Position = 2;
        this.vbxConfig.Add(this.vbxSample);
        global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.vbxSample]));
        w5.Position = 0;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.vbxGA = new global::Gtk.VBox();
        this.vbxGA.WidthRequest = 250;
        this.vbxGA.Name = "vbxGA";
        this.vbxGA.Spacing = 6;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.vbbSample = new global::Gtk.VButtonBox();
        this.vbbSample.Name = "vbbSample";
        this.vbxGA.Add(this.vbbSample);
        global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.vbbSample]));
        w6.Position = 0;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator1 = new global::Gtk.HSeparator();
        this.hseparator1.Name = "hseparator1";
        this.vbxGA.Add(this.hseparator1);
        global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator1]));
        w7.Position = 1;
        w7.Expand = false;
        w7.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label1 = new global::Gtk.Label();
        this.label1.Name = "label1";
        this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Population size");
        this.vbxGA.Add(this.label1);
        global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label1]));
        w8.Position = 2;
        w8.Expand = false;
        w8.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox2 = new global::Gtk.HBox();
        this.hbox2.Name = "hbox2";
        this.hbox2.Spacing = 6;
        // Container child hbox2.Gtk.Box+BoxChild
        this.label2 = new global::Gtk.Label();
        this.label2.Name = "label2";
        this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Min");
        this.hbox2.Add(this.label2);
        global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label2]));
        w9.Position = 0;
        w9.Expand = false;
        w9.Fill = false;
        // Container child hbox2.Gtk.Box+BoxChild
        this.sbtPopulationMinSize = new global::Gtk.SpinButton(2, 10000, 1);
        this.sbtPopulationMinSize.CanFocus = true;
        this.sbtPopulationMinSize.Name = "sbtPopulationMinSize";
        this.sbtPopulationMinSize.Adjustment.PageIncrement = 10;
        this.sbtPopulationMinSize.ClimbRate = 1;
        this.sbtPopulationMinSize.Numeric = true;
        this.sbtPopulationMinSize.Value = 50;
        this.hbox2.Add(this.sbtPopulationMinSize);
        global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.sbtPopulationMinSize]));
        w10.Position = 1;
        w10.Expand = false;
        w10.Fill = false;
        w10.Padding = ((uint)(3));
        // Container child hbox2.Gtk.Box+BoxChild
        this.hbox3 = new global::Gtk.HBox();
        this.hbox3.Name = "hbox3";
        this.hbox3.Spacing = 6;
        // Container child hbox3.Gtk.Box+BoxChild
        this.label3 = new global::Gtk.Label();
        this.label3.Name = "label3";
        this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Max");
        this.hbox3.Add(this.label3);
        global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label3]));
        w11.Position = 0;
        w11.Expand = false;
        w11.Fill = false;
        // Container child hbox3.Gtk.Box+BoxChild
        this.sbtPopulationMaxSize = new global::Gtk.SpinButton(2, 10000, 1);
        this.sbtPopulationMaxSize.CanFocus = true;
        this.sbtPopulationMaxSize.Name = "sbtPopulationMaxSize";
        this.sbtPopulationMaxSize.Adjustment.PageIncrement = 10;
        this.sbtPopulationMaxSize.ClimbRate = 1;
        this.sbtPopulationMaxSize.Numeric = true;
        this.sbtPopulationMaxSize.Value = 70;
        this.hbox3.Add(this.sbtPopulationMaxSize);
        global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.sbtPopulationMaxSize]));
        w12.Position = 1;
        w12.Expand = false;
        w12.Fill = false;
        this.hbox2.Add(this.hbox3);
        global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.hbox3]));
        w13.PackType = ((global::Gtk.PackType)(1));
        w13.Position = 2;
        w13.Expand = false;
        w13.Fill = false;
        this.vbxGA.Add(this.hbox2);
        global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox2]));
        w14.Position = 3;
        w14.Expand = false;
        w14.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox11 = new global::Gtk.HBox();
        this.hbox11.Name = "hbox11";
        this.hbox11.Spacing = 6;
        // Container child hbox11.Gtk.Box+BoxChild
        this.cmbGenerationStrategy = global::Gtk.ComboBox.NewText();
        this.cmbGenerationStrategy.WidthRequest = 200;
        this.cmbGenerationStrategy.Name = "cmbGenerationStrategy";
        this.hbox11.Add(this.cmbGenerationStrategy);
        global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.cmbGenerationStrategy]));
        w15.Position = 0;
        w15.Expand = false;
        w15.Fill = false;
        // Container child hbox11.Gtk.Box+BoxChild
        this.btnEditGenerationStrategy = new global::Gtk.Button();
        this.btnEditGenerationStrategy.CanFocus = true;
        this.btnEditGenerationStrategy.Name = "btnEditGenerationStrategy";
        this.btnEditGenerationStrategy.UseUnderline = true;
        this.btnEditGenerationStrategy.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox11.Add(this.btnEditGenerationStrategy);
        global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.btnEditGenerationStrategy]));
        w16.Position = 1;
        w16.Expand = false;
        w16.Fill = false;
        this.vbxGA.Add(this.hbox11);
        global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox11]));
        w17.Position = 4;
        w17.Expand = false;
        w17.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator2 = new global::Gtk.HSeparator();
        this.hseparator2.Name = "hseparator2";
        this.vbxGA.Add(this.hseparator2);
        global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator2]));
        w18.Position = 5;
        w18.Expand = false;
        w18.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label5 = new global::Gtk.Label();
        this.label5.Name = "label5";
        this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Selection");
        this.vbxGA.Add(this.label5);
        global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label5]));
        w19.Position = 6;
        w19.Expand = false;
        w19.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox5 = new global::Gtk.HBox();
        this.hbox5.Name = "hbox5";
        this.hbox5.Spacing = 6;
        // Container child hbox5.Gtk.Box+BoxChild
        this.cmbSelection = global::Gtk.ComboBox.NewText();
        this.cmbSelection.WidthRequest = 200;
        this.cmbSelection.Name = "cmbSelection";
        this.hbox5.Add(this.cmbSelection);
        global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.cmbSelection]));
        w20.Position = 0;
        w20.Expand = false;
        w20.Fill = false;
        // Container child hbox5.Gtk.Box+BoxChild
        this.btnEditSelection = new global::Gtk.Button();
        this.btnEditSelection.CanFocus = true;
        this.btnEditSelection.Name = "btnEditSelection";
        this.btnEditSelection.UseUnderline = true;
        this.btnEditSelection.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox5.Add(this.btnEditSelection);
        global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.btnEditSelection]));
        w21.Position = 1;
        w21.Expand = false;
        w21.Fill = false;
        this.vbxGA.Add(this.hbox5);
        global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox5]));
        w22.Position = 7;
        w22.Expand = false;
        w22.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator4 = new global::Gtk.HSeparator();
        this.hseparator4.Name = "hseparator4";
        this.vbxGA.Add(this.hseparator4);
        global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator4]));
        w23.Position = 8;
        w23.Expand = false;
        w23.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label7 = new global::Gtk.Label();
        this.label7.Name = "label7";
        this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Crossover");
        this.vbxGA.Add(this.label7);
        global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label7]));
        w24.Position = 9;
        w24.Expand = false;
        w24.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox9 = new global::Gtk.HBox();
        this.hbox9.Name = "hbox9";
        this.hbox9.Spacing = 6;
        // Container child hbox9.Gtk.Box+BoxChild
        this.label10 = new global::Gtk.Label();
        this.label10.Name = "label10";
        this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("Probability");
        this.hbox9.Add(this.label10);
        global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.label10]));
        w25.Position = 0;
        w25.Expand = false;
        w25.Fill = false;
        // Container child hbox9.Gtk.Box+BoxChild
        this.hslCrossoverProbability = new global::Gtk.HScale(null);
        this.hslCrossoverProbability.CanFocus = true;
        this.hslCrossoverProbability.Name = "hslCrossoverProbability";
        this.hslCrossoverProbability.Adjustment.Upper = 1.1;
        this.hslCrossoverProbability.Adjustment.PageIncrement = 0.1;
        this.hslCrossoverProbability.Adjustment.PageSize = 0.1;
        this.hslCrossoverProbability.Adjustment.StepIncrement = 0.01;
        this.hslCrossoverProbability.Adjustment.Value = 0.01;
        this.hslCrossoverProbability.DrawValue = true;
        this.hslCrossoverProbability.Digits = 2;
        this.hslCrossoverProbability.ValuePos = ((global::Gtk.PositionType)(2));
        this.hbox9.Add(this.hslCrossoverProbability);
        global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.hslCrossoverProbability]));
        w26.Position = 1;
        this.vbxGA.Add(this.hbox9);
        global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox9]));
        w27.Position = 10;
        w27.Expand = false;
        w27.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox6 = new global::Gtk.HBox();
        this.hbox6.Name = "hbox6";
        this.hbox6.Spacing = 6;
        // Container child hbox6.Gtk.Box+BoxChild
        this.cmbCrossover = global::Gtk.ComboBox.NewText();
        this.cmbCrossover.WidthRequest = 200;
        this.cmbCrossover.Name = "cmbCrossover";
        this.hbox6.Add(this.cmbCrossover);
        global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.cmbCrossover]));
        w28.Position = 0;
        w28.Expand = false;
        w28.Fill = false;
        // Container child hbox6.Gtk.Box+BoxChild
        this.btnEditCrossover = new global::Gtk.Button();
        this.btnEditCrossover.CanFocus = true;
        this.btnEditCrossover.Name = "btnEditCrossover";
        this.btnEditCrossover.UseUnderline = true;
        this.btnEditCrossover.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox6.Add(this.btnEditCrossover);
        global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.btnEditCrossover]));
        w29.Position = 1;
        w29.Expand = false;
        w29.Fill = false;
        this.vbxGA.Add(this.hbox6);
        global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox6]));
        w30.Position = 11;
        w30.Expand = false;
        w30.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator5 = new global::Gtk.HSeparator();
        this.hseparator5.Name = "hseparator5";
        this.vbxGA.Add(this.hseparator5);
        global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator5]));
        w31.Position = 12;
        w31.Expand = false;
        w31.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label8 = new global::Gtk.Label();
        this.label8.Name = "label8";
        this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("Mutation");
        this.vbxGA.Add(this.label8);
        global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label8]));
        w32.Position = 13;
        w32.Expand = false;
        w32.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox8 = new global::Gtk.HBox();
        this.hbox8.Name = "hbox8";
        this.hbox8.Spacing = 6;
        // Container child hbox8.Gtk.Box+BoxChild
        this.label9 = new global::Gtk.Label();
        this.label9.Name = "label9";
        this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("Probability");
        this.hbox8.Add(this.label9);
        global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.label9]));
        w33.Position = 0;
        w33.Expand = false;
        w33.Fill = false;
        // Container child hbox8.Gtk.Box+BoxChild
        this.hslMutationProbability = new global::Gtk.HScale(null);
        this.hslMutationProbability.CanFocus = true;
        this.hslMutationProbability.Name = "hslMutationProbability";
        this.hslMutationProbability.Adjustment.Upper = 1.1;
        this.hslMutationProbability.Adjustment.PageIncrement = 0.1;
        this.hslMutationProbability.Adjustment.PageSize = 0.1;
        this.hslMutationProbability.Adjustment.StepIncrement = 0.01;
        this.hslMutationProbability.DrawValue = true;
        this.hslMutationProbability.Digits = 2;
        this.hslMutationProbability.ValuePos = ((global::Gtk.PositionType)(2));
        this.hbox8.Add(this.hslMutationProbability);
        global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.hslMutationProbability]));
        w34.Position = 1;
        this.vbxGA.Add(this.hbox8);
        global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox8]));
        w35.Position = 14;
        w35.Expand = false;
        w35.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox7 = new global::Gtk.HBox();
        this.hbox7.Name = "hbox7";
        this.hbox7.Spacing = 6;
        // Container child hbox7.Gtk.Box+BoxChild
        this.cmbMutation = global::Gtk.ComboBox.NewText();
        this.cmbMutation.WidthRequest = 200;
        this.cmbMutation.Name = "cmbMutation";
        this.hbox7.Add(this.cmbMutation);
        global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.cmbMutation]));
        w36.Position = 0;
        w36.Expand = false;
        w36.Fill = false;
        // Container child hbox7.Gtk.Box+BoxChild
        this.btnEditMutation = new global::Gtk.Button();
        this.btnEditMutation.CanFocus = true;
        this.btnEditMutation.Name = "btnEditMutation";
        this.btnEditMutation.UseUnderline = true;
        this.btnEditMutation.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox7.Add(this.btnEditMutation);
        global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.btnEditMutation]));
        w37.Position = 1;
        w37.Expand = false;
        w37.Fill = false;
        this.vbxGA.Add(this.hbox7);
        global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox7]));
        w38.Position = 15;
        w38.Expand = false;
        w38.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator3 = new global::Gtk.HSeparator();
        this.hseparator3.Name = "hseparator3";
        this.vbxGA.Add(this.hseparator3);
        global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator3]));
        w39.Position = 16;
        w39.Expand = false;
        w39.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label12 = new global::Gtk.Label();
        this.label12.Name = "label12";
        this.label12.LabelProp = global::Mono.Unix.Catalog.GetString("Reinsertion");
        this.vbxGA.Add(this.label12);
        global::Gtk.Box.BoxChild w40 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label12]));
        w40.Position = 17;
        w40.Expand = false;
        w40.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox12 = new global::Gtk.HBox();
        this.hbox12.Name = "hbox12";
        this.hbox12.Spacing = 6;
        // Container child hbox12.Gtk.Box+BoxChild
        this.cmbReinsertion = global::Gtk.ComboBox.NewText();
        this.cmbReinsertion.WidthRequest = 200;
        this.cmbReinsertion.Name = "cmbTermination1";
        this.hbox12.Add(this.cmbReinsertion);
        global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.hbox12[this.cmbReinsertion]));
        w41.Position = 0;
        w41.Expand = false;
        w41.Fill = false;
        // Container child hbox12.Gtk.Box+BoxChild
        this.btnEditReinsertion = new global::Gtk.Button();
        this.btnEditReinsertion.CanFocus = true;
        this.btnEditReinsertion.Name = "btnEditReinsertion";
        this.btnEditReinsertion.UseUnderline = true;
        this.btnEditReinsertion.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox12.Add(this.btnEditReinsertion);
        global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.hbox12[this.btnEditReinsertion]));
        w42.Position = 1;
        w42.Expand = false;
        w42.Fill = false;
        this.vbxGA.Add(this.hbox12);
        global::Gtk.Box.BoxChild w43 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox12]));
        w43.Position = 18;
        w43.Expand = false;
        w43.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.label11 = new global::Gtk.Label();
        this.label11.Name = "label11";
        this.label11.LabelProp = global::Mono.Unix.Catalog.GetString("Termination");
        this.vbxGA.Add(this.label11);
        global::Gtk.Box.BoxChild w44 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.label11]));
        w44.Position = 19;
        w44.Expand = false;
        w44.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hbox10 = new global::Gtk.HBox();
        this.hbox10.Name = "hbox10";
        this.hbox10.Spacing = 6;
        // Container child hbox10.Gtk.Box+BoxChild
        this.cmbTermination = global::Gtk.ComboBox.NewText();
        this.cmbTermination.WidthRequest = 200;
        this.cmbTermination.Name = "cmbTermination";
        this.hbox10.Add(this.cmbTermination);
        global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.cmbTermination]));
        w45.Position = 0;
        w45.Expand = false;
        w45.Fill = false;
        // Container child hbox10.Gtk.Box+BoxChild
        this.btnEditTermination = new global::Gtk.Button();
        this.btnEditTermination.CanFocus = true;
        this.btnEditTermination.Name = "btnEditTermination";
        this.btnEditTermination.UseUnderline = true;
        this.btnEditTermination.Label = global::Mono.Unix.Catalog.GetString("Edit");
        this.hbox10.Add(this.btnEditTermination);
        global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.btnEditTermination]));
        w46.Position = 1;
        w46.Expand = false;
        w46.Fill = false;
        this.vbxGA.Add(this.hbox10);
        global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hbox10]));
        w47.Position = 20;
        w47.Expand = false;
        w47.Fill = false;
        // Container child vbxGA.Gtk.Box+BoxChild
        this.hseparator6 = new global::Gtk.HSeparator();
        this.hseparator6.Name = "hseparator6";
        this.vbxGA.Add(this.hseparator6);
        global::Gtk.Box.BoxChild w48 = ((global::Gtk.Box.BoxChild)(this.vbxGA[this.hseparator6]));
        w48.Position = 21;
        w48.Expand = false;
        w48.Fill = false;
        this.vbxConfig.Add(this.vbxGA);
        global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.vbxGA]));
        w49.Position = 1;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.btnStart = new global::Gtk.Button();
        this.btnStart.CanFocus = true;
        this.btnStart.Name = "btnStart";
        this.btnStart.UseUnderline = true;
        this.btnStart.Label = global::Mono.Unix.Catalog.GetString("_Start");
        this.vbxConfig.Add(this.btnStart);
        global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.btnStart]));
        w50.Position = 2;
        w50.Expand = false;
        w50.Fill = false;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.btnNew = new global::Gtk.Button();
        this.btnNew.CanFocus = true;
        this.btnNew.Name = "btnNew";
        this.btnNew.UseUnderline = true;
        this.btnNew.Label = global::Mono.Unix.Catalog.GetString("_New");
        this.vbxConfig.Add(this.btnNew);
        global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.btnNew]));
        w51.Position = 3;
        w51.Expand = false;
        w51.Fill = false;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.btnStop = new global::Gtk.Button();
        this.btnStop.CanFocus = true;
        this.btnStop.Name = "btnStop";
        this.btnStop.UseUnderline = true;
        this.btnStop.Label = global::Mono.Unix.Catalog.GetString("_Stop");
        this.vbxConfig.Add(this.btnStop);
        global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.btnStop]));
        w52.Position = 4;
        w52.Expand = false;
        w52.Fill = false;
        // Container child vbxConfig.Gtk.Box+BoxChild
        this.btnResume = new global::Gtk.Button();
        this.btnResume.CanFocus = true;
        this.btnResume.Name = "btnResume";
        this.btnResume.UseUnderline = true;
        this.btnResume.Label = global::Mono.Unix.Catalog.GetString("_Resume");
        this.vbxConfig.Add(this.btnResume);
        global::Gtk.Box.BoxChild w53 = ((global::Gtk.Box.BoxChild)(this.vbxConfig[this.btnResume]));
        w53.Position = 5;
        w53.Expand = false;
        w53.Fill = false;
        this.hbxMain.Add(this.vbxConfig);
        global::Gtk.Box.BoxChild w54 = ((global::Gtk.Box.BoxChild)(this.hbxMain[this.vbxConfig]));
        w54.Position = 1;
        w54.Expand = false;
        w54.Fill = false;
        this.Add(this.hbxMain);
        if ((this.Child != null))
        {
            this.Child.ShowAll();
        }
        this.btnResume.Hide();
        this.Show();
        this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
    }
}
