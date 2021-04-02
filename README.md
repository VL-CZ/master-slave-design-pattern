# Example of Master-slave design pattern implementation for matrix multiplication

## Introduction

This repository contains an example of master-slave design pattern,
used for matrix multiplication.

There is a Matrix class (immutable from outside) and two matrix calculators in the project. \
`SimpleMatrixCalculator` goes row by row and column by column and calculates
the result. \
`ParallelMatrixCalculator` uses master-slave design pattern and firstly 
divides the work for slaves: each slave calculates multiplication of 1 row 
with the second matrix (e.g. one row of the product) and starts them **in parallel** (using `Tasks`)
Then, it waits until all the slaves finish, creates a product from the slave results and returns it.

Both matrix calculators inherit from abstract class `MatrixCalculator` which contains common methods for all of the calculators.

There is also a simple benchamrk in `Master_slave_design_pattern.Benchmark` project. You can set matrix dimension, number of repetitions there and compare
the time of both calculators.

## Requirements
- .NET 5.0

## Run
### Simple example
```shell
dotnet run --project Master_slave_design_pattern
```

### Benchmark
```shell
dotnet run --project Master_slave_design_pattern.Benchmark
```

## Benchmark result
- average computation times measured with randomly generated matrices of the given size with 10 repetitions

### My PC

| Matrix size | simple algorithm time (ms) | parallel algorithm time (ms) |
| :---------: | :------------------------: | :--------------------------: |
|   10 x 10   |             4              |              10              |
|   50 x 50   |             8              |              16              |
|  100 x 100  |             37             |              30              |
|  200 x 200  |            356             |             157              |
|  300 x 300  |            702             |             314              |
|  500 x 500  |            2540            |             1256             |

### MFF lab (u-pl0)

| Matrix size | simple algorithm time (ms) | parallel algorithm time (ms) |
| :---------: | :------------------------: | :--------------------------: |
|   10 x 10   |            < 1             |             < 1              |
|   50 x 50   |             1              |             < 1              |
|  100 x 100  |             11             |              4               |
|  200 x 200  |             89             |              28              |
|  300 x 300  |            266             |              89              |
|  500 x 500  |            1215            |             428              |