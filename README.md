## Example of Master-slave design pattern implementation for matrix multiplication

### Simple benchmark
- average computation times measured with randomly generated matrices of the given size with 10 repetitions

| Matrix size | simple algorithm time (ms) | parallel algorithm time (ms) |
| ----------- | -------------------------- | ---------------------------- |
| 10 x 10     | 7                          | 22                           |
| 50 x 50     | 10                         | 33                           |
| 100 x 100   | 36                         | 44                           |
| 200 x 200   | 265                        | 142                          |
| 300 x 300   | 586                        | 301                          |
| 500 x 500   | 2466                       | 1195                         |