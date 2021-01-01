
var aData,aBillTag,aBillDate;
var aLabels, aTags, aDates;

//Product Wise Data
var HiddenValue_1 = document.getElementById('hfLabels').value;
aLabels = HiddenValue_1.split(",");
var HiddenValue_2 = document.getElementById('hfBillsProduct').value;
aData = HiddenValue_2.split("[");
aData = aData[2].split(",");
for (var i = 0; i < aData.length; i++)
{
    aData[i] = parseFloat(aData[i], 10);
    if (Number.isNaN(aData[i]))
        aData[i] = 0;
}

//Tag Wise Data
var HiddenValue_3 = document.getElementById('hfTags').value;
aTags = HiddenValue_3.split(",");
var HiddenValue_4 = document.getElementById('hfBillsTags').value;
aBillTag = HiddenValue_4.split("[");
aBillTag = aBillTag[2].split(",");
for (var i = 0; i < aBillTag.length; i++) {
    aBillTag[i] = parseFloat(aBillTag[i], 10);
    if (Number.isNaN(aBillTag[i]))
        aBillTag[i] = 0;
}

//Date Wise Data
var HiddenValue_5 = document.getElementById('hfDates').value;
aDates = HiddenValue_5.split(",");
var HiddenValue_6 = document.getElementById('hfBillsDates').value;
aBillDate = HiddenValue_6.split("[");
aBillDate = aBillDate[2].split(",");
for (var i = 0; i < aBillDate.length; i++) {
    aBillDate[i] = parseFloat(aBillDate[i], 10);
    if (Number.isNaN(aBillDate[i]))
        aBillDate[i] = 0;
}

var randomScalingFactor = function () { return Math.round(Math.random() * 1000) };
var getColor = function() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
};

    var lineChartData = {
        labels: aLabels,
		    datasets : [
			    {
				    label: "My First dataset",
				    fillColor : "rgba(220,220,220,0.2)",
				    strokeColor : "rgba(220,220,220,1)",
				    pointColor : "rgba(220,220,220,1)",
				    pointStrokeColor : "#fff",
				    pointHighlightFill : "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: aData
			    },
			    {
				    label: "My Second dataset",
				    fillColor : "rgba(48, 164, 255, 0.2)",
				    strokeColor : "rgba(48, 164, 255, 1)",
				    pointColor : "rgba(48, 164, 255, 1)",
				    pointStrokeColor : "#fff",
				    pointHighlightFill : "#fff",
                    pointHighlightStroke: "rgba(48, 164, 255, 1)",
                    data: aData
			    }
		    ]

    }

var lineChartDataTag = {
    labels: aTags,
        datasets: [
            {
                label: "My First dataset",
                fillColor: "rgba(220,220,220,0.2)",
                strokeColor: "rgba(220,220,220,1)",
                pointColor: "rgba(220,220,220,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: aBillTag
            },
            {
                label: "My Second dataset",
                fillColor: "rgba(48, 164, 255, 0.2)",
                strokeColor: "rgba(48, 164, 255, 1)",
                pointColor: "rgba(48, 164, 255, 1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(48, 164, 255, 1)",
                data: aBillTag
            }
        ]

    }
		
    var barChartData = {
        labels: aLabels,
		    datasets : [
			    {
				    fillColor : "rgba(48, 164, 255, 0.2)",
				    strokeColor : "rgba(48, 164, 255, 0.8)",
				    highlightFill : "rgba(48, 164, 255, 0.75)",
                    highlightStroke: "rgba(48, 164, 255, 1)",
                    data: aData
			    }
		    ]
    }

var barChartDataDate = {
    labels: aDates,
    datasets: [
        {
            fillColor: "rgba(48, 164, 255, 0.2)",
            strokeColor: "rgba(48, 164, 255, 0.8)",
            highlightFill: "rgba(48, 164, 255, 0.75)",
            highlightStroke: "rgba(48, 164, 255, 1)",
            data: aBillDate
        }
    ]
}

var pieData = [
    {
        value: 300,
        color: "#30a5ff",
        highlight: "#62b9fb",
        label: "Blue"
    },
    {
        value: 50,
        color: "#ffb53e",
        highlight: "#fac878",
        label: "Orange"
    },
    {
        value: 100,
        color: "#1ebfae",
        highlight: "#3cdfce",
        label: "Teal"
    },
    {
        value: 120,
        color: "#f9243f",
        highlight: "#f6495f",
        label: "Red"
    }

];
			
	var doughnutData = [
				{
					value: 300,
					color:"#30a5ff",
					highlight: "#62b9fb",
					label: "Blue"
				},
				{
					value: 50,
					color: "#ffb53e",
					highlight: "#fac878",
					label: "Orange"
				},
				{
					value: 100,
					color: "#1ebfae",
					highlight: "#3cdfce",
					label: "Teal"
				},
				{
					value: 120,
					color: "#f9243f",
					highlight: "#f6495f",
					label: "Red"
				}

			];
			
	var radarData = {
	    labels: ["Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running"],
	    datasets: [
	        {
	            label: "My First dataset",
	            fillColor: "rgba(220,220,220,0.2)",
	            strokeColor: "rgba(220,220,220,1)",
	            pointColor: "rgba(220,220,220,1)",
	            pointStrokeColor: "#fff",
	            pointHighlightFill: "#fff",
	            pointHighlightStroke: "rgba(220,220,220,1)",
	            data: [65, 59, 90, 81, 56, 55, 40]
	        },
	        {
	            label: "My Second dataset",
	            fillColor : "rgba(48, 164, 255, 0.2)",
	            strokeColor : "rgba(48, 164, 255, 0.8)",
	            pointColor : "rgba(48, 164, 255, 1)",
	            pointStrokeColor : "#fff",
	            pointHighlightFill : "#fff",
	            pointHighlightStroke : "rgba(48, 164, 255, 1)",
	            data: [28, 48, 40, 19, 96, 27, 100]
	        }
	    ]
	};
	
	var polarData = [
		    {
		    	value: 300,
		    	color: "#1ebfae",
		    	highlight: "#38cabe",
		    	label: "Teal"
		    },
		    {
		    	value: 140,
		    	color: "#ffb53e",
		    	highlight: "#fac878",
		    	label: "Orange"
		    },
		    {
		    	value: 220,
		    	color:"#30a5ff",
		    	highlight: "#62b9fb",
		    	label: "Blue"
		    },
		    {
		    	value: 250,
		    	color: "#f9243f",
		    	highlight: "#f6495f",
		    	label: "Red"
		    }
		
	];

