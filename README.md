## Example of Master-slave design pattern implementation for matrix multiplication

### Simple benchmark
- average computation times measured with randomly generated matrices of the given size with 10 repetitions

| Matrix size | simple algorithm time (ms) | parallel algorithm time (ms) |
| ----------- | -------------------------- | ---------------------------- |
| 10 x 10     | 4                          | 10                           |
| 50 x 50     | 8                          | 16                           |
| 100 x 100   | 37                         | 30                           |
| 200 x 200   | 356                        | 157                          |
| 300 x 300   | 702                        | 314                          |
| 500 x 500   | 2540                       | 1256                         |
