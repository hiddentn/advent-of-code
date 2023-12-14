"use strict";
var __spreadArray = (this && this.__spreadArray) || function (to, from, pack) {
    if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
        if (ar || !(i in from)) {
            if (!ar) ar = Array.prototype.slice.call(from, 0, i);
            ar[i] = from[i];
        }
    }
    return to.concat(ar || Array.prototype.slice.call(from));
};
Object.defineProperty(exports, "__esModule", { value: true });
var fs = require("fs");
var Range = /** @class */ (function () {
    function Range(start, end, isTransformed) {
        if (isTransformed === void 0) { isTransformed = false; }
        this.start = start;
        this.end = end;
        this.isTransformed = isTransformed;
    }
    Object.defineProperty(Range.prototype, "length", {
        get: function () {
            return this.end - this.start;
        },
        enumerable: false,
        configurable: true
    });
    Range.prototype.getIntersection = function (range) {
        if (this.end <= range.start || this.start >= range.end)
            return null;
        return new Range(Math.max(this.start, range.start), Math.min(this.end, range.end));
    };
    Range.prototype.subtractIntersection = function (intersection) {
        var result = [];
        if (this.start < intersection.start) {
            result.push(new Range(this.start, intersection.start));
        }
        if (this.end > intersection.end) {
            result.push(new Range(intersection.end, this.end));
        }
        return result;
    };
    return Range;
}());
var GardenMap = /** @class */ (function () {
    function GardenMap(mapStr) {
        var _a = mapStr
            .split(" ")
            .filter(function (str) { return str !== ""; })
            .map(Number), destinationStart = _a[0], sourceStart = _a[1], length = _a[2];
        this.destination = new Range(destinationStart, destinationStart + length);
        this.source = new Range(sourceStart, sourceStart + length);
    }
    Object.defineProperty(GardenMap.prototype, "offset", {
        get: function () {
            return this.destination.start - this.source.start;
        },
        enumerable: false,
        configurable: true
    });
    GardenMap.prototype.transformRange = function (inputRange) {
        if (inputRange.isTransformed)
            return [inputRange];
        var intersection = this.source.getIntersection(inputRange);
        if (!intersection)
            return [inputRange];
        var transformed = new Range(intersection.start + this.offset, intersection.end + this.offset, true);
        return __spreadArray([transformed], inputRange.subtractIntersection(intersection), true);
    };
    return GardenMap;
}());
var parseSeedRanges = function (line) {
    var numbers = line
        .split(":")[1]
        .split(" ")
        .filter(function (number) { return number !== ""; })
        .map(Number);
    var ranges = [];
    for (var i = 0; i < numbers.length; i += 2) {
        ranges.push(new Range(numbers[i], numbers[i] + numbers[i + 1]));
    }
    return ranges;
};
var parseGardenMapGroups = function (lines) {
    var gardenMapGroups = [];
    lines.forEach(function (line) {
        if (line === "") {
            gardenMapGroups.push([]);
            return;
        }
        if (line.endsWith(":"))
            return;
        gardenMapGroups[gardenMapGroups.length - 1].push(new GardenMap(line));
    });
    return gardenMapGroups;
};
var resetTransformed = function (ranges) {
    for (var _i = 0, ranges_1 = ranges; _i < ranges_1.length; _i++) {
        var range = ranges_1[_i];
        range.isTransformed = false;
    }
};
var calculateGardenMapGroupTransform = function (ranges, gardenMapGroup) {
    var _loop_1 = function (gardenMap) {
        ranges = ranges.flatMap(function (range) { return gardenMap.transformRange(range); });
    };
    for (var _i = 0, gardenMapGroup_1 = gardenMapGroup; _i < gardenMapGroup_1.length; _i++) {
        var gardenMap = gardenMapGroup_1[_i];
        _loop_1(gardenMap);
    }
    return ranges;
};
var calculateSeedLocation = function (ranges, gardenMapGroups) {
    for (var i = 0, gardenMapGroups_1 = gardenMapGroups; i < gardenMapGroups_1.length; i++) {
        var gardenMapGroup = gardenMapGroups_1[i];
        resetTransformed(ranges);
        ranges = calculateGardenMapGroupTransform(ranges, gardenMapGroup);
    }
    return ranges;
};
var getMin = function (ranges) {
    var min = Infinity;
    for (var i = 0, ranges_2 = ranges; i < ranges_2.length; i++) {
        var range = ranges_2[i];
        min = Math.min(min, range.start);
    }
    return min;
};
var input = fs.readFileSync("input.txt", "utf8");
var lines = input.split("\n").map(function (line) { return line.trim(); });
var seedRanges = parseSeedRanges(lines[0]);
var gardenMapGroups = parseGardenMapGroups(lines.slice(1));
var result = getMin(calculateSeedLocation(seedRanges, gardenMapGroups));
console.log(result);
