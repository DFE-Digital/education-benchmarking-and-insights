Feature: Local Authority high needs national rankings

    @HighNeedsFlagEnabled
    Scenario: National rankings displays stubbed data in table
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on table view
        Then the national rankings table is displayed with the following values:
          | Name                | Value  |
          | Local authority 202 | 40%    |
          | Local authority 201 | 43.8%  |
          | Local authority 205 | 53.1%  |
          | Local authority 352 | 53.7%  |
          | Local authority 349 | 53.8%  |
          | Local authority 346 | 53.9%  |
          | Local authority 343 | 54%    |
          | Local authority 354 | 54%    |
          | Local authority 340 | 54.1%  |
          | Local authority 351 | 54.1%  |
          | Local authority 337 | 54.2%  |
          | Local authority 348 | 54.2%  |
          | Local authority 334 | 54.3%  |
          | Local authority 345 | 54.3%  |
          | Local authority 331 | 54.4%  |
          | Local authority 342 | 54.4%  |
          | Local authority 353 | 54.4%  |
          | Local authority 328 | 54.5%  |
          | Local authority 339 | 54.5%  |
          | Local authority 350 | 54.5%  |
          | Local authority 325 | 54.6%  |
          | Local authority 336 | 54.6%  |
          | Local authority 347 | 54.6%  |
          | Local authority 322 | 54.7%  |
          | Local authority 333 | 54.7%  |
          | Local authority 344 | 54.7%  |
          | Local authority 319 | 54.8%  |
          | Local authority 330 | 54.8%  |
          | Local authority 341 | 54.9%  |
          | Local authority 316 | 54.9%  |
          | Local authority 327 | 55%    |
          | Local authority 338 | 55%    |
          | Local authority 313 | 55%    |
          | Local authority 324 | 55.1%  |
          | Local authority 335 | 55.1%  |
          | Local authority 310 | 55.1%  |
          | Local authority 321 | 55.2%  |
          | Local authority 332 | 55.2%  |
          | Local authority 307 | 55.3%  |
          | Local authority 318 | 55.3%  |
          | Local authority 329 | 55.4%  |
          | Local authority 304 | 55.4%  |
          | Local authority 315 | 55.5%  |
          | Local authority 326 | 55.5%  |
          | Local authority 301 | 55.5%  |
          | Local authority 312 | 55.6%  |
          | Local authority 323 | 55.6%  |
          | Local authority 298 | 55.6%  |
          | Local authority 309 | 55.7%  |
          | Local authority 320 | 55.8%  |
          | Local authority 295 | 55.8%  |
          | Local authority 306 | 55.9%  |
          | Local authority 317 | 55.9%  |
          | Local authority 292 | 55.9%  |
          | Local authority 303 | 56%    |
          | Local authority 314 | 56%    |
          | Local authority 289 | 56.1%  |
          | Local authority 300 | 56.2%  |
          | Local authority 311 | 56.2%  |
          | Local authority 286 | 56.2%  |
          | Local authority 297 | 56.3%  |
          | Local authority 308 | 56.3%  |
          | Local authority 283 | 56.4%  |
          | Local authority 294 | 56.5%  |
          | Local authority 305 | 56.5%  |
          | Local authority 280 | 56.5%  |
          | Local authority 291 | 56.6%  |
          | Local authority 277 | 56.7%  |
          | Local authority 302 | 56.7%  |
          | Local authority 288 | 56.8%  |
          | Local authority 274 | 56.8%  |
          | Local authority 299 | 56.8%  |
          | Local authority 285 | 57%    |
          | Local authority 271 | 57%    |
          | Local authority 296 | 57%    |
          | Local authority 282 | 57.2%  |
          | Local authority 268 | 57.2%  |
          | Local authority 293 | 57.2%  |
          | Local authority 208 | 57.3%  |
          | Local authority 279 | 57.3%  |
          | Local authority 265 | 57.3%  |
          | Local authority 290 | 57.4%  |
          | Local authority 262 | 57.5%  |
          | Local authority 276 | 57.5%  |
          | Local authority 287 | 57.6%  |
          | Local authority 259 | 57.7%  |
          | Local authority 273 | 57.7%  |
          | Local authority 284 | 57.8%  |
          | Local authority 256 | 57.9%  |
          | Local authority 270 | 57.9%  |
          | Local authority 281 | 58%    |
          | Local authority 253 | 58.1%  |
          | Local authority 267 | 58.1%  |
          | Local authority 278 | 58.2%  |
          | Local authority 250 | 58.3%  |
          | Local authority 264 | 58.4%  |
          | Local authority 275 | 58.4%  |
          | Local authority 247 | 58.5%  |
          | Local authority 261 | 58.6%  |
          | Local authority 272 | 58.7%  |
          | Local authority 244 | 58.7%  |
          | Local authority 258 | 58.8%  |
          | Local authority 241 | 58.9%  |
          | Local authority 269 | 58.9%  |
          | Local authority 211 | 59%    |
          | Local authority 255 | 59.1%  |
          | Local authority 238 | 59.1%  |
          | Local authority 266 | 59.2%  |
          | Local authority 235 | 59.3%  |
          | Local authority 252 | 59.4%  |
          | Local authority 263 | 59.5%  |
          | Local authority 232 | 59.5%  |
          | Local authority 249 | 59.6%  |
          | Local authority 214 | 59.7%  |
          | Local authority 229 | 59.7%  |
          | Local authority 260 | 59.8%  |
          | Local authority 226 | 59.9%  |
          | Local authority 246 | 59.9%  |
          | Local authority 217 | 60%    |
          | Local authority 223 | 60%    |
          | Local authority 257 | 60.1%  |
          | Local authority 220 | 60.1%  |
          | Local authority 243 | 60.2%  |
          | Local authority 254 | 60.4%  |
          | Local authority 240 | 60.6%  |
          | Local authority 251 | 60.7%  |
          | Local authority 237 | 60.9%  |
          | Local authority 248 | 61.1%  |
          | Local authority 234 | 61.3%  |
          | Local authority 204 | 61.5%  |
          | Local authority 245 | 61.5%  |
          | Local authority 231 | 61.6%  |
          | Local authority 242 | 61.9%  |
          | Local authority 228 | 62%    |
          | Local authority 239 | 62.4%  |
          | Local authority 225 | 62.5%  |
          | Local authority 236 | 62.9%  |
          | Local authority 222 | 62.9%  |
          | Local authority 219 | 63.4%  |
          | Local authority 233 | 63.4%  |
          | Local authority 216 | 63.8%  |
          | Local authority 207 | 64%    |
          | Local authority 230 | 64%    |
          | Local authority 213 | 64.2%  |
          | Local authority 210 | 64.4%  |
          | Local authority 227 | 64.7%  |
          | Local authority 224 | 65.5%  |
          | Local authority 221 | 66.3%  |
          | Local authority 218 | 67.4%  |
          | Local authority 215 | 68.6%  |
          | Local authority 212 | 70.1%  |
          | Local authority 209 | 72.2%  |
          | Local authority 206 | 75.1%  |
          | Local authority 203 | 80.4%  |
          | Local authority 200 | 113.3% |

    @HighNeedsFlagEnabled
    Scenario: Download national ranking chart
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on save as image
        Then the chart image is downloaded

    @HighNeedsFlagEnabled
    Scenario: Copy national ranking chart
        Given I am on local authority high needs national rankings for local authority with code '204'
        When I click on copy image
        Then the chart image is copied