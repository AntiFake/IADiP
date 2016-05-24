function InitChart(container, jsonJunior, jsonMiddle, jsonSenior) {
    var jsonJuniorData = JSON.parse(jsonJunior.replace(/&quot;/g, '"'));
    var jsonMiddleData = JSON.parse(jsonMiddle.replace(/&quot;/g, '"'));
    var jsonSeniorData = JSON.parse(jsonSenior.replace(/&quot;/g, '"'));

    /*var data = [{
        "sale": "202",
        "year": "1"
    }, {
        "sale": "215",
        "year": "2"
    }, {
        "sale": "179",
        "year": "3"
    }, {
        "sale": "199",
        "year": "4"
    }, {
        "sale": "134",
        "year": "5"
    }, {
        "sale": "176",
        "year": "6"
    }];
    var data2 = [{
        "sale": "152",
        "year": "1"
    }, {
        "sale": "189",
        "year": "2"
    }, {
        "sale": "179",
        "year": "3"
    }, {
        "sale": "199",
        "year": "4"
    }, {
        "sale": "134",
        "year": "5"
    }, {
        "sale": "176",
        "year": "6"
    }];*/
    var vis = d3.select(container),
        WIDTH = 1000,
        HEIGHT = 500,
        MARGINS = {
            top: 20,
            right: 20,
            bottom: 20,
            left: 50
        },
        xScale = d3.scale.linear().range([MARGINS.left, WIDTH - MARGINS.right]).domain([1, 3]),
        yScale = d3.scale.linear().range([HEIGHT - MARGINS.top, MARGINS.bottom]).domain([0, 15]),
        xAxis = d3.svg.axis()
        .scale(xScale)
        .ticks(2),
        yAxis = d3.svg.axis()
        .scale(yScale)
        .orient("left");

    vis.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + (HEIGHT - MARGINS.bottom) + ")")
        .call(xAxis);
    vis.append("svg:g")
        .attr("class", "y axis")
        .attr("transform", "translate(" + (MARGINS.left) + ",0)")
        .call(yAxis);
    var lineGen = d3.svg.line()
        .x(function (d) {
            return xScale(d.month);
        })
        .y(function (d) {
            return yScale(d.sum);
        })
        .interpolate("linear");

    vis.append('svg:path')
        .attr('d', lineGen(jsonJuniorData))
        .attr('stroke', 'green')
        .attr('stroke-width', 2)
        .attr('fill', 'none');
    vis.append('svg:path')
        .attr('d', lineGen(jsonMiddleData))
        .attr('stroke', 'blue')
        .attr('stroke-width', 2)
        .attr('fill', 'none');
    vis.append('svg:path')
        .attr('d', lineGen(jsonSeniorData))
        .attr('stroke', 'red')
        .attr('stroke-width', 2)
        .attr('fill', 'none');
}